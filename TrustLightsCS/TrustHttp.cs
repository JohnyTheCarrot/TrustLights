using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;

namespace TrustLightsCS
{
    public class TrustHttp
    {
        public static string lastURL;

        private const string baseUrl = "https://trustsmartcloud2.com/ics2000_api/";

        private static string GetUrl(string path, TrustHttpParams trustHttpParams)
        {
            return baseUrl + path + trustHttpParams;
        }

        private static (string, HttpStatusCode, string) MakeRequest(string path, TrustHttpParams trustHttpParams)
        {
            var url = GetUrl(path, trustHttpParams);
            lastURL = url;
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            using (var dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                var reader = new StreamReader(dataStream);
                // Read the content.
                var responseFromServer = reader.ReadToEnd();
                // Display the content.
                return (responseFromServer, response.StatusCode, response.StatusDescription);
            }
        }

        public static BaseTrustUser LoginUser(string macAddress, string email, string password)
        {
            var trustParams = new TrustHttpParams
            {
                TrustHttpParamDictionary =
                {
                    ["action"] = "login",
                    ["email"] = email,
                    ["device_unique_id"] = "android",
                    ["platform"] = "Android",
                    ["mac"] = macAddress.Replace(":", ""),
                    ["password_hash"] = password
                }
            };
            var (responseText, code, codeDescription) = MakeRequest("account.php", trustParams);

            if (code == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<BaseTrustUser>(responseText);
            throw new TrustHttpError(codeDescription, code);

        }

        public static void SendCommand(string macAddress, string email, string password, TrustCommand command)
        {
            var trustParams = new TrustHttpParams
            {
                TrustHttpParamDictionary =
                {
                    ["action"] = "add",
                    ["email"] = email,
                    ["device_unique_id"] = "android",
                    ["mac"] = macAddress.Replace(":", ""),
                    ["password_hash"] = password,
                    ["command"] = command.GetCommand()
                }
            };

            var (responseText, _, _) = MakeRequest("command.php", trustParams);

            Debug.WriteLine(responseText);
            //throw new TrustHttpError(codeDescription, code);
        }

        public static BaseTrustDevice[] GetHubDevices(string macAddress, string email, string password)
        {
            var trustParams = new TrustHttpParams
            {
                TrustHttpParamDictionary =
                {
                    ["action"] = "sync",
                    ["email"] = email,
                    ["mac"] = macAddress.Replace(":", ""),
                    ["password_hash"] = password
                }
            };

            var (responseText, code, codeDescription) = MakeRequest("gateway.php", trustParams);

            if (code == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<BaseTrustDevice[]>(responseText);
            throw new TrustHttpError(codeDescription, code);
        }

        public static TrustHub GetHub(string macAddress, string email, string password)
        {
            var trustParams = new TrustHttpParams
            {
                TrustHttpParamDictionary =
                {
                    ["action"] = "check",
                    ["email"] = email,
                    ["mac"] = macAddress.Replace(":", ""),
                    ["password_hash"] = password
                }
            };
            var (_, code, codeDescription) = MakeRequest("gateway.php", trustParams);

            if (code == HttpStatusCode.OK)
                return new TrustHub(macAddress, email, password);
            throw new TrustHttpError(codeDescription, code);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TrustLightsCS
{
    class TrustHttpParams
    {
        public Dictionary<string, string> TrustHttpParamDictionary = new Dictionary<string, string>();

        public override string ToString()
        {
            var keys = TrustHttpParamDictionary.Keys.ToList();
            var values = TrustHttpParamDictionary.Values.ToList();
            var toReturn = "";

            for (var i = 0; i < keys.Count; i++)
            {
                var value = HttpUtility.UrlEncode(values[i]);
                if (i == 0)
                    toReturn += $"?{keys[i]}={value}";
                else
                    toReturn += $"&{keys[i]}={value}";
            }

            return toReturn;
        }
    }
}

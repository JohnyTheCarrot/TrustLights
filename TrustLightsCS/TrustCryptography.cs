using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TrustLightsCS
{
    public class TrustCryptography
    {
        public static string Decrypt(string str, string aes)
        {
            var bytes = Convert.FromBase64String(str);
            var iv = new byte[16];
            Array.Copy(bytes, iv, 16);
            var bytesToDecrypt = new byte[bytes.Length - 16];
            for (var i = 16; i < bytes.Length; i++)
                bytesToDecrypt[i - 16] = bytes[i];
            return DecryptAES(bytesToDecrypt, aes, iv);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] EncryptStringToBytes_Aes(string plainText, string Key)
        {
            byte[] encrypted;
            byte[] IV;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = StringToByteArray(Key);

                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                aesAlg.Mode = CipherMode.CBC;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            var combinedIvCt = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

            // Return the encrypted bytes from the memory stream. 
            return combinedIvCt;

        }

        public static string DecryptAES(byte[] encrypted, string aesKey, byte[] iv)
        {
            string decrypted;

            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = StringToByteArray(aesKey);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var dec = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encrypted))
                {
                    using (var cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }
    }
}

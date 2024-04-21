using ParaTakip.Core;
using System.Security.Cryptography;
using System.Text;

namespace ParaTakip.Common
{
    public class EncriptionHelper
    {
        public static string EncryptResetToken(string email)
        {
            string text = email + "&date=" + DateTime.Now.ToString();
            byte[] iv = new byte[16];
            byte[] array;

            if (!ConfigurationCache.Instance.TryGet("Secret",out string SecretKey))
            {
                throw new AppException(ReturnMessages.SECRET_KEY_NOT_FOUND);
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptResetToken(string token)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(token);
            if (!ConfigurationCache.Instance.TryGet("Secret", out string SecretKey))
            {
                throw new AppException(ReturnMessages.SECRET_KEY_NOT_FOUND);
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }

        }
    }

}

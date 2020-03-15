using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
// ReSharper disable PossibleNullReferenceException

namespace JsonFieldEncrypter.Tools
{
    public static class Encrypter
    {
        private static byte[] GetKeyBytes(string keyText) => Encoding.UTF8.GetBytes(keyText);

        public static string Encrypt(string text, string key)
        {
            using Aes aes = Aes.Create();
            using ICryptoTransform encryptor = aes.CreateEncryptor(GetKeyBytes(key), aes.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using var swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(text);
            }

            byte[] vactor = aes.IV;
            byte[] decryptedContent = msEncrypt.ToArray();
            byte[] result = new byte[decryptedContent.Length + vactor.Length];

            Buffer.BlockCopy(vactor, 0, result, 0, vactor.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, vactor.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string cipherText, string key)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            byte[] vector = aes.IV;
            byte[] cipher = new byte[fullCipher.Length - vector.Length];

            Buffer.BlockCopy(fullCipher, 0, vector, 0, vector.Length);
            Buffer.BlockCopy(fullCipher, vector.Length, cipher, 0, cipher.Length);

            string result;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(GetKeyBytes(key), vector))
            {
                using var msDecrypt = new MemoryStream(cipher);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                result = srDecrypt.ReadToEnd();
            }

            return result;
        }
    }
}
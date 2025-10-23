using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CookCo_opGame
{
    public static class EncryptionUtility
    {
        // 경고: 키를 하드코딩하는 것은 안전하지 않습니다. 실제 프로젝트에서는 더 안전한 키 관리 전략을 고려해야 합니다.
        private static byte[] GetKey()
        {
            // 키를 분리해서 저장하여 난독화
            string keyPart1 = "MySecretKeyForEn";
            string keyPart2 = "cryption12345678";
            return Encoding.UTF8.GetBytes(keyPart1 + keyPart2);
        }

        private static byte[] GetIV()
        {
            // IV를 분리해서 저장하여 난독화
            string ivPart1 = "MySecret";
            string ivPart2 = "IV123456";
            return Encoding.UTF8.GetBytes(ivPart1 + ivPart2);
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetKey();
                aesAlg.IV = GetIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            try
            {
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = GetKey();
                    aesAlg.IV = GetIV();
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                // Base64 문자열이 아닌 경우 (암호화되지 않은 기존 데이터)
                return cipherText;
            }
        }
    }
}

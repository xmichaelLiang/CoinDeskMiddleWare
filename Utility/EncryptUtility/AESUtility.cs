using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility.EncryptUtility
{
    public static class  AesEncryptionService
    {
        public static string Encrypt(string plainText, byte[] key)
        {
              using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                var iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, byte[] key)
        {
            if(string.IsNullOrEmpty(cipherText))
                return string.Empty;

             try{
                   var fullCipher = Convert.FromBase64String(cipherText);
                   return DecryptProcess(fullCipher,key);  
             }
             catch{
                 return string.Empty; 
             }
        }

        private static string DecryptProcess(byte[] fullCipher, byte[] key)
        {
                   using (var aes = Aes.Create())
                    {
                        aes.Key = key;
                        var iv = new byte[aes.BlockSize / 8];
                        var cipher = new byte[fullCipher.Length - iv.Length];

                        Array.Copy(fullCipher, iv, iv.Length);
                        Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                        aes.IV = iv;

                        using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                        using (var ms = new MemoryStream(cipher))
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
        }
    }
}
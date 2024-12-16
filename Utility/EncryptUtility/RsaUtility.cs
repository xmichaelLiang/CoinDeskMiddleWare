using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Utility.EncryptUtility
{
             public static class RsaUtility
            {
                private static RSAParameters _privateKey;
                private static RSAParameters _publicKey;
                 
                static RsaUtility()
                {
                    using (var rsa = new RSACryptoServiceProvider(2048))
                    {
                        _privateKey = rsa.ExportParameters(true);
                        _publicKey = rsa.ExportParameters(true);
                    }
                }

                public static string Encrypt(string data)
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(_publicKey);
                        var dataToEncrypt = Encoding.UTF8.GetBytes(data);
                        var encryptedData = rsa.Encrypt(dataToEncrypt, false);
                        return Convert.ToBase64String(encryptedData);
                    }
                }

                public static string Decrypt(string data)
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(_privateKey);
                        var dataToDecrypt = Convert.FromBase64String(data);
                        var decryptedData = rsa.Decrypt(dataToDecrypt, false);
                        return Encoding.UTF8.GetString(decryptedData);
                    }
                }

                public static RSAParameters GetPublicKey()
                {
                    return _publicKey;
                }

                public static RSAParameters GetPrivateKey()
                {
                    return _privateKey;
                }
   }
}
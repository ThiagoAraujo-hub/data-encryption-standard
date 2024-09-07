using System.Security.Cryptography;
using System.Text;

namespace SHA256.Encrypt
{
    public class DESCripterObsolete
    {
        private static readonly DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);

            using var ms = new MemoryStream();
            using var encryptor = des.CreateEncryptor(key, iv);
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string encryptedText, byte[] key, byte[] iv)
        {
            byte[] encryptedByteArray = Convert.FromBase64String(encryptedText);

            using var ms = new MemoryStream(encryptedByteArray);
            using var decryptor = des.CreateDecryptor(key, iv);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}

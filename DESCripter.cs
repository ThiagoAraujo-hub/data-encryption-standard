using System.Security.Cryptography;
using System.Text;

namespace SHA256.Encrypt
{
    public class DESCripter
    {
        private static readonly DES des = DES.Create();

        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            Validate(key, iv);

            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);

            des.Key = key;
            des.IV = iv;

            using var ms = new MemoryStream();
            using var encryptor = des.CreateEncryptor();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string encryptedText, byte[] key, byte[] iv)
        {
            Validate(key, iv);

            byte[] encryptedByteArray = Convert.FromBase64String(encryptedText);

            des.Key = key;
            des.IV = iv;

            using var ms = new MemoryStream(encryptedByteArray);
            using var decryptor = des.CreateDecryptor();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        private static void Validate(byte[] key, byte[] iv)
        {
            // Check if the key and IV have the correct sizes
            if (key.Length != 8)
                throw new ArgumentException("Key must be 8 bytes for DES encryption.");

            if (iv.Length != 8)
                throw new ArgumentException("IV must be 8 bytes for DES encryption.");
        }
    }
}

using SHA256.Encrypt;
using System.Text;

string originalString = "YourStringHere";

// The key must be 24 bytes and the IV must be 8 bytes
byte[] key = Encoding.UTF8.GetBytes("123456789012345678901234"); // 24 bytes
byte[] iv = Encoding.UTF8.GetBytes("12345678");  // 8 bytes

// Encrypt the original string
string encryptedString = TripleDESCripter.EncryptString(originalString, key, iv);
Console.WriteLine($"Encrypted String: {encryptedString}");

Console.WriteLine();

// Decrypt the encrypted string
string decryptedString = TripleDESCripter.DecryptString(encryptedString, key, iv);
Console.WriteLine($"Decrypted String: {decryptedString}");
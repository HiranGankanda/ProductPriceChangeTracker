using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PPCT.WebApplication.Helpers
{
    public class CipherText
    {
        private const int Keysize = 128;
        private const int DerivationIterations = 1000;
        private const string passPhrase = "WEwCwedcsaweE@!wfdd32";

        public static async Task<string> EncriptAsync(string cipherText)
        {
            return await Task.Run(() => Encrypt(cipherText));
        }
        public static string Encrypt(string cipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return null;

                var saltStringBytes = Generate128BitsOfRandomEntropy();
                var ivStringBytes = Generate128BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(cipherText);
                using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations);
                var keyBytes = password.GetBytes(Keysize / 8);

                using var symmetricKey = Aes.Create();
                symmetricKey.BlockSize = 128;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;

                using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
                using var memoryStream = new MemoryStream();

                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                var cipherTextBytes = saltStringBytes;
                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                return Convert.ToBase64String(cipherTextBytes);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static async Task<string> DecryptAsync(string cipherText)
        {
            return await Task.Run(() => Decrypt(cipherText));
        }
        public static string Decrypt(string cipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return null;

                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations);
                var keyBytes = password.GetBytes(Keysize / 8);

                using var symmetricKey = Aes.Create();
                symmetricKey.BlockSize = 128;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;

                using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
                using var memoryStream = new MemoryStream(cipherTextBytes);

                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                var plainTextBytes = new byte[cipherTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 16 Bytes will give us 128 bits.
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}

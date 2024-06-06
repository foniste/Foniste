using System.Security.Cryptography;
using System.Text;

namespace FonApi.Service
{
    public class EncryptionService
    {
        private static readonly int KeySize = 256;
        private static readonly int BlockSize = 128;
        private static readonly string DefaultHexKey = "29dc126216ae31c45c1908ee156b5192afc331eb1b2a553f3bb3e830862b7932";

        public string Encrypt(string plainText)
        {
            byte[] key = DefaultHexKey == null ? HexStringToByteArray(DefaultHexKey) : HexStringToByteArray(DefaultHexKey);
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = BlockSize;
                aes.Key = key;
                aes.IV = new byte[BlockSize / 8]; // 16 byte IV for AES

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string cipherText, string hexKey = null)
        {
            byte[] key = hexKey == null ? HexStringToByteArray(DefaultHexKey) : HexStringToByteArray(hexKey);
            string decryptedText;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = BlockSize;
                aes.Key = key;
                aes.IV = new byte[BlockSize / 8]; // 16 byte IV for AES

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decryptedText = sr.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedText;
        }

        private byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }

}

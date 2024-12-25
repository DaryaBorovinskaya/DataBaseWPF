using System.Security.Cryptography;
using System.Text;

namespace DataBase1WPF.Models.Encryptors
{
    /// <summary>
    /// Шифрование данных
    /// </summary>
    public class Encryptor : IEncryptor<string, string>
    {
        /// <summary>
        /// Шифрование (хеширование) переданной строки с помощью SHA256
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return Convert.ToHexString(hashValue);
        }
    }
}

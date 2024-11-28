using System.Security.Cryptography;
using System.Text;

namespace DataBase1WPF.Models.Encryptors
{
    public class Encryptor : IEncryptor<string, string>
    {
        public string Encrypt(string value)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return Convert.ToHexString(hashValue);
        }
    }
}

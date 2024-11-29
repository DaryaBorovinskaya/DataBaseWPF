using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.Models.Encryptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.LogIn
{
    public class LogInService : ILogInService
    {
        private IEncryptor<string, string> _encryptor;

        public LogInService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }
        public bool LogIn(string login, string password)
        {
            IList<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read();
            foreach (IUserDB userDB in usersDB)
            {
                if (userDB.Login == login && userDB.Password == _encryptor.Encrypt(password))
                    return true;
            }

            return false;
        }
    }
}

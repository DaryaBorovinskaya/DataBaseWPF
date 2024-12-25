using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.Models.Encryptors;

namespace DataBase1WPF.Models.Services.LogIn
{
    /// <summary>
    /// Сервис для аутентификации пользователя
    /// </summary>
    public class LogInService : ILogInService
    {
        private IEncryptor<string, string> _encryptor;

        public LogInService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }

        /// <summary>
        /// Попытка аутентификации пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogIn(string login, string password)
        {
            IList<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read();
            foreach (IUserDB userDB in usersDB)
            {
                if (userDB.Login == login && userDB.Password == _encryptor.Encrypt(password))
                {
                    DataManager.GetInstance().CurrentUser = userDB;
                    return true;
                }
            }

            return false;
        }
    }
}

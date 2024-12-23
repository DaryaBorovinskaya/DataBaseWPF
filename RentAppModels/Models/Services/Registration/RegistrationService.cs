using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.Models.Encryptors;

namespace DataBase1WPF.Models.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private IEncryptor<string,  string> _encryptor;

        public RegistrationService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }

        public bool Registration(string login, string password)
        {
            IList<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read();
            foreach (IUserDB userDB in usersDB)
            {
                if (userDB.Login == login)
                    return false;
            }

            DataManager.GetInstance().UserDB_Repository.Create(new UserDB(login, _encryptor.Encrypt(password)));
            return true;
        }
    }
}

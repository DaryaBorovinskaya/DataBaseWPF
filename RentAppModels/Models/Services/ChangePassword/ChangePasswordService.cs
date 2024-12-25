using DataBase1WPF.Models.Encryptors;

namespace DataBase1WPF.Models.Services.ChangePassword
{
    /// <summary>
    /// Сервис для смены пароля 
    /// </summary>
    public class ChangePasswordService : IChangePasswordService
    {
        private IEncryptor<string, string> _encryptor;

        public ChangePasswordService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (_encryptor.Encrypt(oldPassword) == DataManager.GetInstance().CurrentUser.Password)
            {
                DataManager.GetInstance().CurrentUser.Password = _encryptor.Encrypt(newPassword);
                DataManager.GetInstance().UserDB_Repository.Update(DataManager.GetInstance().CurrentUser);
            }
            else
            {
                throw new ArgumentException("Неверный прежний пароль");
            }

        }
    }
}

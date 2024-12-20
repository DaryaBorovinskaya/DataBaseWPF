using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.Models.Encryptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.ChangePassword
{
    public class ChangePasswordService : IChangePasswordService
    {
        private IEncryptor<string, string> _encryptor;

        public ChangePasswordService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }
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

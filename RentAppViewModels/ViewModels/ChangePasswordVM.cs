using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.ChangePassword;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    /// <summary>
    /// Обработка и получение данных из окна ChangePasswordWindow
    /// </summary>
    public class ChangePasswordVM : BasicVM
    {
        private string _oldPassword;
        private string _newPassword;
        private string _repeatedPassword;

        public Action OnSuccessChangePassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { Set<string>(ref _oldPassword, value); }
        }

        public string NewPassword
        {
            private get { return _newPassword; }
            set { Set<string>(ref _newPassword, value); }
        }

        public string RepeatedPassword
        {
            private get { return _repeatedPassword; }
            set { Set<string>(ref _repeatedPassword, value); }
        }

        
        /// <summary>
        /// Изменение пароля
        /// </summary>
        public ICommand ClickChange
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    bool isCorrect = true;

                    if (OldPassword == null || OldPassword == string.Empty)
                    {
                        MessageBox.Show("Поле \"Прежний пароль\" не заполнено");
                        isCorrect = false;
                    }

                    else if (NewPassword == null || NewPassword == string.Empty)
                    {
                        MessageBox.Show("Поле \"Новый пароль\" не заполнено");
                        isCorrect = false;
                    }

                    else if (NewPassword != RepeatedPassword)
                    {
                        MessageBox.Show("Поля \"Прежний пароль\" и \"Новый пароль\"" +
                            " не совпадают");
                        isCorrect = false;
                    }

                    if (isCorrect)
                    {
                        ChangePasswordService changePassword = new(new Encryptor());

                        try
                        {
                            changePassword.ChangePassword(OldPassword, NewPassword);
                            MessageBox.Show("Смена пароля прошла успешно");
                            OnSuccessChangePassword?.Invoke();
                        }
                        catch(ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                    }
                });
            }
        }
    }
}

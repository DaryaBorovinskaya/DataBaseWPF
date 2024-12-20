using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Registration;

namespace DataBase1WPF.ViewModels
{
    public class RegistrationVM : BasicVM
    {
        private string _name;
        private string _password;
        private string _repeatedPassword;

        public Action OnSuccessRegistration;
        public string Name
        {
            get { return _name; }
            set { Set<string>(ref _name, value); }
        }

        public string Password
        {
            private get { return _password; }
            set { Set<string>(ref _password, value); }
        }

        public string RepeatedPassword
        {
            private get { return _repeatedPassword; }
            set { Set<string>(ref _repeatedPassword, value); }
        }


        public ICommand ClickRegistrate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    bool isCorrect = true;

                    if (Password == null || Password == string.Empty)
                    {
                        MessageBox.Show("Поле \"Пароль\" не заполнено");
                        isCorrect = false;
                    }

                    else if (RepeatedPassword == null || RepeatedPassword == string.Empty)
                    {
                        MessageBox.Show("Поле \"Повторите пароль\" не заполнено");
                        isCorrect = false;
                    }

                    else if (Password != RepeatedPassword)
                    {
                        MessageBox.Show("Поля \"Пароль\" и \"Повторите пароль\" не совпадают");
                        isCorrect = false;
                    }

                    if (isCorrect)
                    {
                        RegistrationService registrationService = new(new Encryptor());
                        registrationService.Registration(Name, Password);
                        MessageBox.Show("Регистрация прошла успешно");
                        OnSuccessRegistration?.Invoke();
                        Name = string.Empty;
                        Password = null;
                        RepeatedPassword = null;
                    }
                });
            }
        }

        
    }
}

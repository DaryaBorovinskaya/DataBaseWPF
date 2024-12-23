using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.LogIn;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    public class LogInVM : BasicVM
    {
        private string _name;
        private string _password;

        public Action OnLogInSuccess;
        public string Name 
        {
            get { return _name; }
            set { Set<string> (ref _name, value); }
        }

        public string Password
        {
            private get { return _password; }
            set { Set<string>(ref _password, value); }
        }


        public ICommand ClickCancellation
        {
            get 
            {
                return new DelegateCommand((obj) =>
                    {
                        Name = string.Empty;
                    });
            }
        }

        public ICommand ClickLogIn
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    LogInService logIn = new(new Encryptor());

                    if (logIn.LogIn(Name, Password))
                    {
                        OnLogInSuccess?.Invoke();
                    }
                    else
                        MessageBox.Show("Ошибка входа: неверный логин или пароль");
                    
                });
            }
        }

    }
}

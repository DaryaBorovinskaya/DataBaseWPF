using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DataBase1WPF.ViewModels
{
    public class MainVM : BasicVM
    {
        public Action OnRegistration;
        public Action OnChangePassword;
        public Action OnSQLquery;

        public ICommand ClickRegistration
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnRegistration?.Invoke();
                });
            }
        }

        public ICommand ClickChangePassword
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnChangePassword?.Invoke();
                });
            }
        }

        public ICommand ClickSQLquery
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnSQLquery?.Invoke();
                });
            }
        }
    }
}

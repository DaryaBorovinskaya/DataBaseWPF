using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    public class LogInVM : BasicVM
    {
        private string _name;
        private SecureString _password;
        public string Name 
        {
            get { return _name; }
            set { Set<string> (ref _name, value); }
        }

        public SecureString Password
        {
            private get { return _password; }
            set { Set<SecureString>(ref _password, value); }
        }


        public ICommand ClickCancellation
        {
            get 
            {
                return new DelegateCommand((obj) =>
                    {
                        Name = string.Empty;
                        Password = null;
                    });
            }
        }


    }
}

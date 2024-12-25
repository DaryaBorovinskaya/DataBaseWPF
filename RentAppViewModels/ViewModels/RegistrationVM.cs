using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.Registration;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    public class RegistrationVM : BasicVM
    {
        private RegistrationService _service;
        private string _name;
        private string _password;
        private string _repeatedPassword;
        private List<string> _employeesComboBox;
        private int _selectedIndexEmployees;

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

        public List<string> EmployeesComboBox
        {
            get { return _employeesComboBox; }
            set
            {
                Set(ref _employeesComboBox, value);
            }
        }

        public int SelectedIndexEmployees
        {
            get { return _selectedIndexEmployees; }
            set
            {
                Set(ref _selectedIndexEmployees, value);
            }
        }

        public RegistrationVM()
        {
            _service = new(new Encryptor());
            _employeesComboBox = _service.GetEmployees();
        }

        public ICommand ClickRegistrate
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    bool isCorrect = true;

                    if (SelectedIndexEmployees == -1)
                    {
                        MessageBox.Show("ОШИБКА: не выбрано значение сотрудника");
                        isCorrect = false;
                    }

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
                        if (_service.Registration(Name, Password, SelectedIndexEmployees))
                        {
                            MessageBox.Show("Регистрация прошла успешно");
                            OnSuccessRegistration?.Invoke();
                            Name = string.Empty;
                            Password = null;
                            RepeatedPassword = null;
                        }
                        else
                            MessageBox.Show("Пользователь с таким именем уже зарегистрирован");
                    }
                });
            }
        }

        
    }
}

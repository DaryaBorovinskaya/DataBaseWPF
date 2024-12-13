using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.Individual;

namespace DataBase1WPF.ViewModels.Individual
{
    public class AddIndividualVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private string _phoneNumberText;
        private string _surnameText;
        private string _nameText;
        private string _patronymicText;
        private DateTime _dateOfIssue;
        private string _passportSeriesText;
        private string _passportNumberText;
        private string _issuedByText;


        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }


        public DateTime DateOfIssue
        {
            get { return _dateOfIssue; }
            set
            {
                Set(ref _dateOfIssue, value);
            }
        }

        public string PhoneNumberText
        {
            get { return _phoneNumberText; }
            set
            {
                Set(ref _phoneNumberText, value);
            }
        }

        public string SurnameText
        {
            get { return _surnameText; }
            set
            {
                Set(ref _surnameText, value);
            }
        }

        public string NameText
        {
            get { return _nameText; }
            set
            {
                Set(ref _nameText, value);
            }
        }

        public string PatronymicText
        {
            get { return _patronymicText; }
            set
            {
                Set(ref _patronymicText, value);
            }
        }

        public string PassportSeriesText
        {
            get { return _passportSeriesText; }
            set
            {
                Set(ref _passportSeriesText, value);
            }
        }

        public string PassportNumberText
        {
            get { return _passportNumberText; }
            set
            {
                Set(ref _passportNumberText, value);
            }
        }


        public string IssuedByText
        {
            get { return _issuedByText; }
            set
            {
                Set(ref _issuedByText, value);
            }
        }

        public AddIndividualVM(ITableService tableService) 
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";
            _dateOfIssue = DateTime.Now;
        }

        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    
                    if (string.IsNullOrEmpty(SurnameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(NameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PatronymicText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PhoneNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PassportSeriesText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PassportNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(IssuedByText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (DateOfIssue.Date == DateTime.Now.Date)
                        MessageBox.Show("ОШИБКА: поле даты заполнено неверно");

                    else
                    {
                        OnApply?.Invoke(SurnameText + " " + NameText + " "
                            + PatronymicText);
                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is IndividualService service)
                service.Add(SurnameText, NameText, PatronymicText, PhoneNumberText,
                    PassportSeriesText, PassportNumberText, DateOfIssue, IssuedByText);
        }
    }
}

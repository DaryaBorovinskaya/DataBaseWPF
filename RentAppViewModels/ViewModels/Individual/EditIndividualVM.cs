using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.Individual;

namespace DataBase1WPF.ViewModels.Individual
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditIndividualWindow
    /// </summary>
    public class EditIndividualVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
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

        public EditIndividualVM(DataRow row, ITableService tableService)
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";
            
            _surnameText = _row[0].ToString();
            _nameText = _row[1].ToString();
            _patronymicText = _row[2].ToString();
            _phoneNumberText = _row[3].ToString();
            _passportSeriesText = _row[4].ToString();
            _passportNumberText = _row[5].ToString();
            _dateOfIssue = DateTime.Parse(_row[6].ToString());
            _issuedByText = _row[7].ToString();
        }

        /// <summary>
        /// Нажатие на кнопку
        /// </summary>
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

                    else
                    {
                        OnApply?.Invoke(SurnameText + " " + NameText + " "
                            + PatronymicText);
                    }
                });
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        public void Edit()
        {
            if (_tableService is IndividualService service)
                service.Update(_row, SurnameText, NameText, PatronymicText, PhoneNumberText,
                    PassportSeriesText, PassportNumberText, DateOfIssue, IssuedByText);
        }



    }
}

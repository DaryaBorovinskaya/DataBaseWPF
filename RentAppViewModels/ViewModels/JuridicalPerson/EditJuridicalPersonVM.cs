using DataBase1WPF.Models.Services.Tables.Individual;
using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;

namespace DataBase1WPF.ViewModels.JuridicalPerson
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditJuridicalPersonWindow
    /// </summary>
    public class EditJuridicalPersonVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
        private string _windowTitle;
        private string _buttonContent;
        private string _nameOfOrganizationText;
        private string _phoneNumberText;
        private string _directorSurnameText;
        private string _directorNameText;
        private string _directorPatronymicText;
        private string _houseNumberText;
        private string _paymentAccountText;
        private string _individualTaxpayerNumberText;

        private List<string> _districtsComboBox;
        private int _selectedIndexDistricts;
        private int _selectedIndexStreets;
        private List<string> _streetsComboBox;
        private List<string> _banksComboBox;
        private int _selectedIndexBanks;
        public Action<string> OnApply;
        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }


        public string PhoneNumberText
        {
            get { return _phoneNumberText; }
            set
            {
                Set(ref _phoneNumberText, value);
            }
        }

        public string NameOfOrganizationText
        {
            get { return _nameOfOrganizationText; }
            set
            {
                Set(ref _nameOfOrganizationText, value);
            }
        }


        public string DirectorSurnameText
        {
            get { return _directorSurnameText; }
            set
            {
                Set(ref _directorSurnameText, value);
            }
        }

        public string DirectorNameText
        {
            get { return _directorNameText; }
            set
            {
                Set(ref _directorNameText, value);
            }
        }

        public string DirectorPatronymicText
        {
            get { return _directorPatronymicText; }
            set
            {
                Set(ref _directorPatronymicText, value);
            }
        }


        public List<string> DistrictsComboBox
        {
            get { return _districtsComboBox; }
            set
            {
                Set(ref _districtsComboBox, value);
            }
        }

        public int SelectedIndexDistricts
        {
            get { return _selectedIndexDistricts; }
            set
            {
                Set(ref _selectedIndexDistricts, value);
            }
        }


        public List<string> StreetsComboBox
        {
            get { return _streetsComboBox; }
            set
            {
                Set(ref _streetsComboBox, value);
            }
        }

        public int SelectedIndexStreets
        {
            get { return _selectedIndexStreets; }
            set
            {
                Set(ref _selectedIndexStreets, value);
            }
        }


        public List<string> BanksComboBox
        {
            get { return _banksComboBox; }
            set
            {
                Set(ref _banksComboBox, value);
            }
        }

        public int SelectedIndexBanks
        {
            get { return _selectedIndexBanks; }
            set
            {
                Set(ref _selectedIndexBanks, value);
            }
        }



        public string HouseNumberText
        {
            get { return _houseNumberText; }
            set
            {
                Set(ref _houseNumberText, value);
            }
        }

        public string PaymentAccountText
        {
            get { return _paymentAccountText; }
            set
            {
                Set(ref _paymentAccountText, value);
            }
        }

        public string IndividualTaxpayerNumberText
        {
            get { return _individualTaxpayerNumberText; }
            set
            {
                Set(ref _individualTaxpayerNumberText, value);
            }
        }

        public EditJuridicalPersonVM(DataRow row, ITableService tableService)
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";

            if (_tableService is JuridicalPersonService service)
            {
                _districtsComboBox = service.GetDistricts();
                _streetsComboBox = service.GetStreets();
                _banksComboBox = service.GetBanks();
                _selectedIndexDistricts = service.GetDistrictSelectedIndex(row);
                _selectedIndexStreets = service.GetStreetsSelectedIndex(row);
                _selectedIndexBanks = service.GetBanksSelectedIndex(row);
            }

            _nameOfOrganizationText = row[0].ToString();
            _directorSurnameText = row[1].ToString();
            _directorNameText = row[2].ToString();
            _directorPatronymicText = row[3].ToString();
            _houseNumberText = row[6].ToString();
            _phoneNumberText = row[7].ToString();
            _paymentAccountText = row[8].ToString();
            _individualTaxpayerNumberText = row[10].ToString();

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
                    if (SelectedIndexDistricts == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение района");
                    else if (SelectedIndexStreets == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение улицы");
                    else if (SelectedIndexBanks == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение банка");
                    else if (string.IsNullOrEmpty(NameOfOrganizationText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(DirectorSurnameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(DirectorNameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(DirectorPatronymicText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(HouseNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PhoneNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PaymentAccountText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(IndividualTaxpayerNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");

                    else
                    {
                        OnApply?.Invoke(NameOfOrganizationText);
                    }
                });
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        public void Edit()
        {
            if (_tableService is JuridicalPersonService service)
                service.Update(_row, SelectedIndexDistricts, SelectedIndexStreets, SelectedIndexBanks,
                    NameOfOrganizationText, DirectorSurnameText, DirectorNameText,
                    DirectorPatronymicText, HouseNumberText, PhoneNumberText,
                    PaymentAccountText, IndividualTaxpayerNumberText);
        }
    }
}

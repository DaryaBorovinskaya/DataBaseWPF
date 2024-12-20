﻿using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Individual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.Employee;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;

namespace DataBase1WPF.ViewModels.JuridicalPerson
{
    public class AddJuridicalPersonVM : BasicVM
    {
        private ITableService _tableService;
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
        
        public AddJuridicalPersonVM(ITableService tableService)
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";

            if (_tableService is JuridicalPersonService service)
            {
                _districtsComboBox = service.GetDistricts();
                _streetsComboBox = service.GetStreets();
                _banksComboBox = service.GetBanks();
            }

        }

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

        public void Add()
        {
            if (_tableService is JuridicalPersonService service)
                service.Add(SelectedIndexDistricts, SelectedIndexStreets, SelectedIndexBanks,
                    NameOfOrganizationText, DirectorSurnameText, DirectorNameText,
                    DirectorPatronymicText, HouseNumberText, PhoneNumberText,
                    PaymentAccountText, IndividualTaxpayerNumberText);
            SelectedIndexDistricts = -1;
            SelectedIndexStreets = -1;
            SelectedIndexBanks = -1;
            NameOfOrganizationText = string.Empty;
            DirectorSurnameText = string.Empty;
            DirectorNameText = string.Empty;
            DirectorPatronymicText = string.Empty;
            HouseNumberText = string.Empty;
            PhoneNumberText = string.Empty;
            PaymentAccountText = string.Empty;
            IndividualTaxpayerNumberText = string.Empty;
        }
    }
}
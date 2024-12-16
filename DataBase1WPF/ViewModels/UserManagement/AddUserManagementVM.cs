using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;
using DataBase1WPF.Models.Services.Tables.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DataBase1WPF.ViewModels.UserManagement
{
    public class AddUserManagementVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;

        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }

        public AddUserManagementVM(ITableService tableService)
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";

            if (_tableService is UserManagementService service)
            {
                //_districtsComboBox = service.GetDistricts();
                //_streetsComboBox = service.GetStreets();
                //_banksComboBox = service.GetBanks();
            }
        }


        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    //if (SelectedIndexDistricts == -1)
                    //    MessageBox.Show("ОШИБКА: не выбрано значение района");
                    //else if (SelectedIndexStreets == -1)
                    //    MessageBox.Show("ОШИБКА: не выбрано значение улицы");
                    //else if (SelectedIndexBanks == -1)
                    //    MessageBox.Show("ОШИБКА: не выбрано значение банка");
                    //else if (string.IsNullOrEmpty(NameOfOrganizationText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(DirectorSurnameText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(DirectorNameText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(DirectorPatronymicText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(HouseNumberText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(PhoneNumberText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(PaymentAccountText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(IndividualTaxpayerNumberText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");

                    //else
                    //{
                    //    OnApply?.Invoke(NameOfOrganizationText);
                    //}
                });
            }
        }

        public void Add()
        {
            if (_tableService is UserManagementService service)
            //    service.Add(SelectedIndexDistricts, SelectedIndexStreets, SelectedIndexBanks,
            //        NameOfOrganizationText, DirectorSurnameText, DirectorNameText,
            //        DirectorPatronymicText, HouseNumberText, PhoneNumberText,
            //        PaymentAccountText, IndividualTaxpayerNumberText);
            //SelectedIndexDistricts = -1;
            //SelectedIndexStreets = -1;
            //SelectedIndexBanks = -1;
            //NameOfOrganizationText = string.Empty;
            //DirectorSurnameText = string.Empty;
            //DirectorNameText = string.Empty;
            //DirectorPatronymicText = string.Empty;
            //HouseNumberText = string.Empty;
            //PhoneNumberText = string.Empty;
            //PaymentAccountText = string.Empty;
            //IndividualTaxpayerNumberText = string.Empty;
        }

    }
}

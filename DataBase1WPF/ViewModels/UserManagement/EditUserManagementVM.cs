using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.UserManagement;

namespace DataBase1WPF.ViewModels.UserManagement
{
    public class EditUserManagementVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
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

        public EditUserManagementVM(DataRow row, ITableService tableService)
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";

            if (_tableService is UserManagementService service)
            {
                //_districtsComboBox = service.GetDistricts();
                //_streetsComboBox = service.GetStreets();
                //_banksComboBox = service.GetBanks();
                //_selectedIndexDistricts = service.GetDistrictSelectedIndex(row);
                //_selectedIndexStreets = service.GetStreetsSelectedIndex(row);
                //_selectedIndexBanks = service.GetBanksSelectedIndex(row);
            }
            //_nameOfOrganizationText = row[0].ToString();
            //_directorSurnameText = row[1].ToString();
            //_directorNameText = row[2].ToString();
            //_directorPatronymicText = row[3].ToString();
            //_houseNumberText = row[6].ToString();
            //_phoneNumberText = row[7].ToString();
            //_paymentAccountText = row[8].ToString();
            //_individualTaxpayerNumberText = row[10].ToString();
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

        public void Edit()
        {
            //if (_tableService is UserManagementService service)
                //service.Update(_row, SelectedIndexDistricts, SelectedIndexStreets, SelectedIndexBanks,
                //    NameOfOrganizationText, DirectorSurnameText, DirectorNameText,
                //    DirectorPatronymicText, HouseNumberText, PhoneNumberText,
                //    PaymentAccountText, IndividualTaxpayerNumberText);
        }





    }
}

using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using DataBase1WPF.Models.Services.Tables.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DataBase1WPF.ViewModels.Contract
{
    public class EditContractVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
        private string _windowTitle;
        private string _buttonContent;

        public Action<string> OnApply;
        public EditContractVM(DataRow row, ITableService tableService) 
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";

            if (_tableService is ContractService service)
            {
                //_districtsComboBox = service.GetDistricts();
                //_streetsComboBox = service.GetStreets();
                //_selectedIndexDistricts = service.GetDistrictSelectedIndex(row);
                //_selectedIndexStreets = service.GetStreetsSelectedIndex(row);
                //_houseNumberText = row[2].ToString();
                //_numberOfFloorsText = row[3].ToString();
                //_countRentalPremisesText = row[4].ToString();
                //_commandantPhoneNumberText = row[5].ToString();
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
                    //else if (string.IsNullOrEmpty(HouseNumberText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(NumberOfFloorsText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(CountRentalPremisesText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");
                    //else if (string.IsNullOrEmpty(CommandantPhoneNumberText))
                    //    MessageBox.Show("ОШИБКА: пустое поле");


                    //else
                    //{
                    //    uint valueNumberOfFloors = CheckValuesUint(NumberOfFloorsText);
                    //    uint valueCountRentalPremises = CheckValuesUint(CountRentalPremisesText);

                    //    if (valueNumberOfFloors != 0 && valueCountRentalPremises != 0)
                    //    {
                    //        if (!CheckValueNumberOfFloors(valueNumberOfFloors))
                    //            MessageBox.Show("ОШИБКА: число больше 255");
                    //        else if (!CheckValueCountRentalPremises(valueCountRentalPremises))
                    //            MessageBox.Show("ОШИБКА: число больше 65535");
                    //        else
                    //            OnApply?.Invoke(
                    //                DistrictsComboBox[SelectedIndexDistricts] + " "
                    //                + StreetsComboBox[SelectedIndexStreets] + " "
                    //                + HouseNumberText);
                    //    }

                    //}

                });
            }
        }

        public void Edit()
        {
            //if (_tableService is BuildingService service)
            //    service.Update(_row, SelectedIndexDistricts, SelectedIndexStreets, HouseNumberText,
            //        uint.Parse(NumberOfFloorsText), uint.Parse(CountRentalPremisesText),
            //        CommandantPhoneNumberText);
        }
    }
}

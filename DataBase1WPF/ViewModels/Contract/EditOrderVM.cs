using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public class EditOrderVM : BasicVM
    {
        private DataRow _row;
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;

        public Action<string> OnApply;
        public EditOrderVM(DataRow row, ITableService tableService)
        {
            _row = row;
            _tableService = tableService;

            _buttonContent = "Внести изменения";

            if (_tableService is BuildingService service)
            {
                _windowTitle = $"Изменение данных таблицы: {service.GetOtherTableName()}";
                //_typeOfFinishingComboBox = service.GetTypesOfFinishing();
                //_buildingText = service.GetSelectedBuildingText();
                //_selectedIndexTypeOfFinishing = service.GetTypeOfFinishingSelectedIndex(row);
                //_premiseNumberText = row[1].ToString();
                //_areaText = row[2].ToString();
                //_floorNumberText = row[3].ToString();
                //_availabilityOfPhoneNumber = bool.Parse(row[4].ToString());
                //_tempRentalPaymentText = row[5].ToString();
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

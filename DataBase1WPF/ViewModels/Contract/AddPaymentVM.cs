using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public class AddPaymentVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;

        public Action<string> OnApply;
        public AddPaymentVM(ITableService tableService)
        {
            _tableService = tableService;

            _buttonContent = "Добавить";

            if (_tableService is BuildingService service)
            {
                _windowTitle = $"Добавление в таблицу: {service.GetOtherTableName()}";
                //_typeOfFinishingComboBox = service.GetTypesOfFinishing();
                //_buildingText = service.GetSelectedBuildingText();
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

        public void Add()
        {
            //if (_tableService is BuildingService service)
            //    service.Add(SelectedIndexDistricts, SelectedIndexStreets, HouseNumberText,
            //        uint.Parse(NumberOfFloorsText), uint.Parse(CountRentalPremisesText),
            //        CommandantPhoneNumberText);
            //SelectedIndexDistricts = -1;
            //SelectedIndexStreets = -1;
            //HouseNumberText = string.Empty;
            //NumberOfFloorsText = string.Empty;
            //CountRentalPremisesText = string.Empty;
            //CommandantPhoneNumberText = string.Empty;
        }
    }
}

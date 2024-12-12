using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DataBase1WPF.ViewModels.Employee
{
    public class AddWorkRecordCardVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private string _buildingText;
        private string _premiseNumberText;
        private string _areaText;
        private string _tempRentalPaymentText;
        private string _floorNumberText;
        private bool _availabilityOfPhoneNumber;

        private List<string> _typeOfFinishingComboBox;
        private int _selectedIndexTypeOfFinishing;
        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }

        public string BuildingText
        {
            get { return _buildingText; }
            set
            {
                Set(ref _buildingText, value);
            }
        }


        public List<string> TypeOfFinishingIdComboBox
        {
            get { return _typeOfFinishingComboBox; }
            set
            {
                Set(ref _typeOfFinishingComboBox, value);
            }
        }

        public int SelectedIndexTypeOfFinishing
        {
            get { return _selectedIndexTypeOfFinishing; }
            set
            {
                Set(ref _selectedIndexTypeOfFinishing, value);
            }
        }


        public string PremiseNumberText
        {
            get { return _premiseNumberText; }
            set
            {
                Set(ref _premiseNumberText, value);
            }
        }

        public string AreaText
        {
            get { return _areaText; }
            set
            {
                Set(ref _areaText, value);
            }
        }

        public string FloorNumberText
        {
            get { return _floorNumberText; }
            set
            {
                Set(ref _floorNumberText, value);
            }
        }


        public bool AvailabilityOfPhoneNumber
        {
            get { return _availabilityOfPhoneNumber; }
            set
            {
                Set(ref _availabilityOfPhoneNumber, value);
            }
        }

        public string TempRentalPaymentText
        {
            get { return _tempRentalPaymentText; }
            set
            {
                Set(ref _tempRentalPaymentText, value);
            }
        }
        public AddWorkRecordCardVM(ITableService tableService)
        {
            _tableService = tableService;

            _buttonContent = "Добавить";

            if (_tableService is BuildingService service)
            {
                _windowTitle = $"Добавление в таблицу: {service.GetOtherTableName()}";
                _typeOfFinishingComboBox = service.GetTypesOfFinishing();
                _buildingText = service.GetSelectedBuildingText();
            }
        }

        private float CheckValuesFloat(string line)
        {
            try
            {
                float value = float.Parse(line);
                if (value > 0)
                    return value;
                else
                {
                    MessageBox.Show("ОШИБКА: введенное значение меньше или равно 0");

                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ОШИБКА: введенное значение не является целым или дробным числом");
                return 0;
            }
        }
        private int CheckValuesInt(string line)
        {
            try
            {
                int value = int.Parse(line);
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ОШИБКА: введенное значение не является целым числом");
                return int.MinValue;
            }
        }

        private bool CheckValueFloorNumber(int value)
        {
            if (value <= 255)
                return true;
            return false;
        }


        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexTypeOfFinishing == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение вида отделки");
                    else if (string.IsNullOrEmpty(PremiseNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(AreaText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(FloorNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(TempRentalPaymentText))
                        MessageBox.Show("ОШИБКА: пустое поле");


                    else
                    {
                        int valueFloorNumber = CheckValuesInt(FloorNumberText);
                        float valueArea = CheckValuesFloat(AreaText);
                        float valueTempRentalPayment = CheckValuesFloat(TempRentalPaymentText);

                        if (valueFloorNumber != int.MinValue && valueArea != 0
                            && valueTempRentalPayment != 0)
                        {
                            if (!CheckValueFloorNumber(valueFloorNumber))
                                MessageBox.Show("ОШИБКА: число больше 255");
                            else
                                OnApply?.Invoke(
                                    TypeOfFinishingIdComboBox[SelectedIndexTypeOfFinishing] + " номер "
                                    + PremiseNumberText + " площадь "
                                    + AreaText);
                        }

                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is BuildingService service)
                service.AddPremises(SelectedIndexTypeOfFinishing, PremiseNumberText,
                    float.Parse(AreaText), int.Parse(FloorNumberText), AvailabilityOfPhoneNumber,
                    float.Parse(TempRentalPaymentText));
        }
    }
}

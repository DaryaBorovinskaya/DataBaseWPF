﻿using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Building
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditBuildingWindow
    /// </summary>
    public class AddBuildingVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private string _houseNumberText;
        private string _numberOfFloorsText;
        private string _countRentalPremisesText;
        private string _commandantPhoneNumberText;

        private List<string> _districtsComboBox;
        private int _selectedIndexDistricts;
        private int _selectedIndexStreets;
        private List<string> _streetsComboBox;
        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
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

        public string HouseNumberText
        {
            get { return _houseNumberText; }
            set
            {
                Set(ref _houseNumberText, value);
            }
        }

        public string NumberOfFloorsText
        {
            get { return _numberOfFloorsText; }
            set
            {
                Set(ref _numberOfFloorsText, value);
            }
        }

        public string CountRentalPremisesText
        {
            get { return _countRentalPremisesText; }
            set
            {
                Set(ref _countRentalPremisesText, value);
            }
        }

        public string CommandantPhoneNumberText
        {
            get { return _commandantPhoneNumberText; }
            set
            {
                Set(ref _commandantPhoneNumberText, value);
            }
        }

        public AddBuildingVM(ITableService tableService)
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";

            if (_tableService is BuildingService service)
            {
                _districtsComboBox = service.GetDistricts();
                _streetsComboBox = service.GetStreets();
            }
        }

        /// <summary>
        /// Валидация ( тип данных uint)  
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private uint CheckValuesUint(string line)
        {
            try
            {
                uint value = uint.Parse(line);
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
                MessageBox.Show("ОШИБКА: введенное значение не является целым положительным числом");
                return 0;
            }
        }

        /// <summary>
        /// Проверка значения количества этажей
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckValueNumberOfFloors(uint value)
        {
            if (value <= 255)
                return true;
            return false;
        }

        /// <summary>
        /// Проверка значения количества помещений для аренды
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckValueCountRentalPremises(uint value)
        {
            if (value <= 65535)
                return true;
            return false;
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
                    else if (string.IsNullOrEmpty(HouseNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(NumberOfFloorsText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(CountRentalPremisesText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(CommandantPhoneNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");


                    else
                    {
                        uint valueNumberOfFloors = CheckValuesUint(NumberOfFloorsText);
                        uint valueCountRentalPremises = CheckValuesUint(CountRentalPremisesText);

                        if (valueNumberOfFloors != 0 && valueCountRentalPremises != 0)
                        {
                            if (!CheckValueNumberOfFloors(valueNumberOfFloors))
                                MessageBox.Show("ОШИБКА: число больше 255");
                            else if (!CheckValueCountRentalPremises(valueCountRentalPremises))
                                MessageBox.Show("ОШИБКА: число больше 65535");
                            else
                                OnApply?.Invoke(
                                    DistrictsComboBox[SelectedIndexDistricts] + " "
                                    + StreetsComboBox[SelectedIndexStreets] + " "
                                    + HouseNumberText);
                        }

                    }
                });
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        public void Add()
        {
            if (_tableService is BuildingService service)
                service.Add(SelectedIndexDistricts, SelectedIndexStreets, HouseNumberText,
                    uint.Parse(NumberOfFloorsText), uint.Parse(CountRentalPremisesText),
                    CommandantPhoneNumberText);
            SelectedIndexDistricts = -1;
            SelectedIndexStreets = -1;
            HouseNumberText = string.Empty;
            NumberOfFloorsText = string.Empty;
            CountRentalPremisesText = string.Empty;
            CommandantPhoneNumberText = string.Empty;
        }
    }
}

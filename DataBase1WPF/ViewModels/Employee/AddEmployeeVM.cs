using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.Employee;

namespace DataBase1WPF.ViewModels.Employee
{
    public class AddEmployeeVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private string _houseNumberText;
        private string _surnameText;
        private string _nameText;
        private string _patronymicText;
        private DateTime _dateOfBirth;

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

        //public DateTime DisplayDateOfBirth
        //{
        //    get { return DateTime.Now; }
        //}
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                Set(ref _dateOfBirth, value);   
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

        public string HouseNumberText
        {
            get { return _houseNumberText; }
            set
            {
                Set(ref _houseNumberText, value);
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


        public AddEmployeeVM(ITableService tableService)
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";

            _dateOfBirth = DateTime.Now;
            if (_tableService is EmployeeService service)
            {
                _districtsComboBox = service.GetDistricts();
                _streetsComboBox = service.GetStreets();
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
                    else if (string.IsNullOrEmpty(HouseNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(SurnameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(NameText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(PatronymicText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (DateOfBirth.Date == DateTime.Now.Date)
                        MessageBox.Show("ОШИБКА: поле даты заполнено неверно");


                    else
                    {
                        OnApply?.Invoke(SurnameText + " " + NameText + " "
                            + PatronymicText);
                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is EmployeeService service)
                service.Add(SelectedIndexDistricts, SelectedIndexStreets, SurnameText,
                    NameText, PatronymicText, DateOfBirth, HouseNumberText);
        }
    }
}

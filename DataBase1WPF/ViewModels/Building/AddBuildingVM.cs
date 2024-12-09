using DataBase1WPF.Models.Services.Tables.Handbooks;
using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DataBase1WPF.ViewModels.Building
{
    public class AddBuildingVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _titleText;
        private string _buttonContent;
        private Visibility _salaryVisibility;
        private string _title;
        private string _salary;

        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }
        public string TitleText
        {
            get { return _titleText; }
        }

        public Visibility SalaryVisibility
        {
            get { return _salaryVisibility; }
            set
            {
                Set(ref _salaryVisibility, value);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
            }
        }

        public string Salary
        {
            get { return _salary; }
            set
            {
                Set(ref _salary, value);

            }
        }


        public AddBuildingVM(ITableService tableService)
        {
            _tableService = tableService;
            _windowTitle = $"Добавление в таблицу: {_tableService.GetTableName()}";
            _buttonContent = "Добавить";
            _salaryVisibility = _tableService.GetTableName() == (new PositionsService()).GetTableName()
                ? Visibility.Visible
                : Visibility.Collapsed;
            _titleText = _tableService.GetTableName() == (new FineService()).GetTableName()
                ? "Сумма"
                : "Наименование";
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

                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ОШИБКА: введенное значение не является целым или дробным числом");
                return -1;
            }
        }

        private bool CheckLengthString(string line)
        {
            if (line.Length > 50)
                return false;
            return true;
        }

        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (string.IsNullOrEmpty(Title))
                        MessageBox.Show("ОШИБКА: пустое поле");

                    //else if(!CheckLengthString(Title))
                    //    MessageBox.Show("ОШИБКА: длина введенного значения больше 50 символов");


                    else if (_tableService is PositionsService positionsService)
                    {
                        float value = CheckValuesFloat(Salary);
                        if (value != -1)
                        {
                            OnApply?.Invoke(value.ToString());
                        }
                    }
                    else if (_tableService is FineService fineService)
                    {
                        float value = CheckValuesFloat(Title);
                        if (value != -1)
                        {
                            OnApply?.Invoke(value.ToString());
                        }
                    }
                    else
                    {
                        OnApply?.Invoke(Title);
                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is PositionsService positionsService)
            {

                positionsService.Add(Title, float.Parse(Salary));


            }
            else if (_tableService is FineService fineService)
            {

                fineService.Add(float.Parse(Title));


            }
            else if (_tableService is BanksService bankService)
            {
                bankService.Add(Title);

            }
            else if (_tableService is DistrictsService districtService)
            {
                districtService.Add(Title);

            }
            else if (_tableService is PaymentFrequencyService paymentFrequencyService)
            {
                paymentFrequencyService.Add(Title);

            }
            else if (_tableService is RentalPurposesService rentalPurposesService)
            {
                rentalPurposesService.Add(Title);

            }
            else if (_tableService is StreetsService streetsService)
            {
                streetsService.Add(Title);

            }
            else if (_tableService is TypesOfFinishingService typesOfFinishingService)
            {
                typesOfFinishingService.Add(Title);

            }
        }
    }
}

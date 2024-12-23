using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Handbooks;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Handbooks
{
    public class EditHandbookVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
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
        public EditHandbookVM(DataRow row, int selectedIndex, ITableService tableService) 
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";
            if (_tableService.GetTableName() == (new PositionsService()).GetTableName())
            {
                _salaryVisibility = Visibility.Visible;
                _salary = row[1].ToString();
            }
            else
            {
                _salaryVisibility = Visibility.Collapsed;
            }
            _titleText = _tableService.GetTableName() == (new FineService()).GetTableName()
               ? "Сумма"
               : "Наименование";
            _title = row[0].ToString();
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

        public void Edit()
        {
            if (_tableService is PositionsService positionsService)
            {
                positionsService.Update(_row, Title, float.Parse(Salary));
                    
            }
            else if (_tableService is FineService fineService)
            {
                fineService.Update(_row, float.Parse(Title));
                
            }
            else if (_tableService is BanksService bankService)
            {
                bankService.Update(_row, Title);
                
            }
            else if (_tableService is DistrictsService districtService)
            {
                districtService.Update(_row, Title);
                
            }
            else if (_tableService is PaymentFrequencyService paymentFrequencyService)
            {
                paymentFrequencyService.Update(_row, Title);
                
            }
            else if (_tableService is RentalPurposesService rentalPurposesService)
            {
                rentalPurposesService.Update(_row, Title);
                
            }
            else if (_tableService is StreetsService streetsService)
            {
                streetsService.Update(_row, Title);
                
            }
            else if (_tableService is TypesOfFinishingService typesOfFinishingService)
            {
                typesOfFinishingService.Update(_row, Title);
                
            }
        }
    }
}

using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Handbooks;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Handbooks
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditHandbookWindow
    /// </summary>
    public class AddHandbookVM : BasicVM
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


        public AddHandbookVM(ITableService tableService) 
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

        /// <summary>
        /// Валидация ( тип данных float)  
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
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

        
        /// <summary>
        /// Нажатие на кнопку
        /// </summary>
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

        /// <summary>
        /// Добавление
        /// </summary>
        public void Add()
        {
            if (_tableService is PositionsService positionsService)
            {
                
                positionsService.Add(Title, float.Parse(Salary));
                Title = string.Empty;
                Salary = string.Empty;
            }
            else if (_tableService is FineService fineService)
            {
                
                fineService.Add(float.Parse(Title));
                Title = string.Empty;

            }
            else if (_tableService is BanksService bankService)
            {
                bankService.Add(Title);
                Title = string.Empty;
            }
            else if (_tableService is DistrictsService districtService)
            {
                districtService.Add(Title);
                Title = string.Empty;
            }
            else if (_tableService is PaymentFrequencyService paymentFrequencyService)
            {
                paymentFrequencyService.Add(Title);
                Title = string.Empty;
            }
            else if (_tableService is RentalPurposesService rentalPurposesService)
            {
                rentalPurposesService.Add(Title);
                Title = string.Empty;
            }
            else if (_tableService is StreetsService streetsService)
            {
                streetsService.Add(Title);
                Title = string.Empty;
            }
            else if (_tableService is TypesOfFinishingService typesOfFinishingService)
            {
                typesOfFinishingService.Add(Title);
                Title = string.Empty;
            }
        }

    }
}

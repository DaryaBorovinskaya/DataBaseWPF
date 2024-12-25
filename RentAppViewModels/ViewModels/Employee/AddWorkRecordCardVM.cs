using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Employee;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Employee
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditWorkRecordCardWindow
    /// </summary>
    public class AddWorkRecordCardVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private string _employeeText;
        private string _orderNumberText;
        private DateTime _orderDate;
        private string _reasonOfRecordingText;

        private List<string> _positionsComboBox;
        private int _selectedIndexPositions;
        public Action<string> OnApply;

        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }

        public string EmployeeText
        {
            get { return _employeeText; }
            set
            {
                Set(ref _employeeText, value);
            }
        }


        public List<string> PositionsComboBox
        {
            get { return _positionsComboBox; }
            set
            {
                Set(ref _positionsComboBox, value);
            }
        }

        public int SelectedIndexPositions
        {
            get { return _selectedIndexPositions; }
            set
            {
                Set(ref _selectedIndexPositions, value);
            }
        }


        public string OrderNumberText
        {
            get { return _orderNumberText; }
            set
            {
                Set(ref _orderNumberText, value);
            }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                Set(ref _orderDate, value);
            }
        }

        public string ReasonOfRecordingText
        {
            get { return _reasonOfRecordingText; }
            set
            {
                Set(ref _reasonOfRecordingText, value);
            }
        }


        
        public AddWorkRecordCardVM(ITableService tableService)
        {
            _tableService = tableService;
            _buttonContent = "Добавить";
            _orderDate = DateTime.Now;

            if (_tableService is EmployeeService service)
            {
                _windowTitle = $"Добавление в таблицу: {service.GetOtherTableName()}";
                _positionsComboBox = service.GetPositions();
                _employeeText = service.GetSelectedEmployeeText();
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
                    if (_selectedIndexPositions == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение должности");
                    else if (string.IsNullOrEmpty(OrderNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");
                    else if (string.IsNullOrEmpty(ReasonOfRecordingText))
                        MessageBox.Show("ОШИБКА: пустое поле");


                    else
                    {
                        OnApply?.Invoke(
                            PositionsComboBox[SelectedIndexPositions] + " номер приказа "
                            + OrderNumberText + " дата "
                            + OrderDate.ToString("dd.MM.yyyy"));
                    }
                });
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        public void Add()
        {
            if (_tableService is EmployeeService service)
                service.AddWorkRecordCard(SelectedIndexPositions, OrderNumberText,
                    OrderDate, ReasonOfRecordingText);

            SelectedIndexPositions = -1;
            OrderNumberText = string.Empty;
            OrderDate = DateTime.Now;
            ReasonOfRecordingText = string.Empty;
        }
    }
}

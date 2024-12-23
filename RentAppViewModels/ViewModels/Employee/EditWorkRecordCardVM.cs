using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Employee;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Employee
{
    public class EditWorkRecordCardVM : BasicVM
    {
        private DataRow _row;
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

        public EditWorkRecordCardVM(DataRow row, ITableService tableService)
        {
            _row = row;
            _tableService = tableService;

            _buttonContent = "Внести изменения";

            if (_tableService is EmployeeService service)
            {
                _windowTitle = $"Изменение данных таблицы: {service.GetOtherTableName()}";
                _positionsComboBox = service.GetPositions();
                _employeeText = service.GetSelectedEmployeeText();
                _selectedIndexPositions = service.GetPositionsSelectedIndex(row);
                _orderNumberText = row[0].ToString();
                _orderDate = DateTime.Parse(row[1].ToString());
                _reasonOfRecordingText = row[3].ToString();
            }
        }


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



        public void Edit()
        {
            if (_tableService is EmployeeService service)
                service.UpdateWorkRecordCard(_row, SelectedIndexPositions, OrderNumberText,
                    OrderDate, ReasonOfRecordingText);
        }
    }
}

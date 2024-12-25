using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    /// <summary>
    /// Обработка и получение данных из окна AddOrEditContractWindow
    /// </summary>
    public class EditContractVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
        private string _windowTitle;
        private string _buttonContent;

        private string _registrationNumberText;
        private DateTime _beginOfActionDate;
        private DateTime _endOfActionDate;
        private string _additionalConditionsText;

        private List<string> _paymentFrequenciesComboBox;
        private int _selectedIndexPaymentFrequencies;
        private List<string> _employeesComboBox;
        private int _selectedIndexEmployees;
        private List<string> _fineComboBox;
        private int _selectedIndexFine;

        public Action<string> OnApply;


        public string RegistrationNumberText
        {
            get { return _registrationNumberText; }
            set
            {
                Set(ref _registrationNumberText, value);
            }
        }

        public DateTime BeginOfActionDate
        {
            get { return _beginOfActionDate; }
            set
            {
                Set(ref _beginOfActionDate, value);
            }
        }


        public DateTime EndOfActionDate
        {
            get { return _endOfActionDate; }
            set
            {
                Set(ref _endOfActionDate, value);
            }
        }



        public List<string> PaymentFrequenciesComboBox
        {
            get { return _paymentFrequenciesComboBox; }
            set
            {
                Set(ref _paymentFrequenciesComboBox, value);
            }
        }

        public int SelectedIndexPaymentFrequencies
        {
            get { return _selectedIndexPaymentFrequencies; }
            set
            {
                Set(ref _selectedIndexPaymentFrequencies, value);
            }
        }


        public List<string> EmployeesComboBox
        {
            get { return _employeesComboBox; }
            set
            {
                Set(ref _employeesComboBox, value);
            }
        }

        public int SelectedIndexEmployees
        {
            get { return _selectedIndexEmployees; }
            set
            {
                Set(ref _selectedIndexEmployees, value);
            }
        }

        public List<string> FineComboBox
        {
            get { return _fineComboBox; }
            set
            {
                Set(ref _fineComboBox, value);
            }
        }

        public int SelectedIndexFine
        {
            get { return _selectedIndexFine; }
            set
            {
                Set(ref _selectedIndexFine, value);
            }
        }


        public string AdditionalConditionsText
        {
            get { return _additionalConditionsText; }
            set
            {
                Set(ref _additionalConditionsText, value);
            }
        }




        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }




        public EditContractVM(DataRow row, ITableService tableService) 
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";

            if (_tableService is ContractService service)
            {
                _paymentFrequenciesComboBox = service.GetPaymentFrequencies();
                _employeesComboBox = service.GetEmployees();
                _fineComboBox = service.GetFines();
                _selectedIndexPaymentFrequencies = service.GetPaymentFrequencySelectedIndex(row);
                _selectedIndexEmployees = service.GetEmployeeSelectedIndex(row);
                _selectedIndexFine = service.GetFineSelectedIndex(row);
                _registrationNumberText = row[0].ToString();
                _beginOfActionDate = DateTime.Parse(row[1].ToString());
                _endOfActionDate = DateTime.Parse(row[2].ToString());
                _additionalConditionsText = row[7].ToString();
                
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
                    if (SelectedIndexPaymentFrequencies == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение периодичности оплаты");
                    else if (SelectedIndexEmployees == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение сотрудника");
                    else if (SelectedIndexFine == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение штрафа");
                    else if (string.IsNullOrEmpty(RegistrationNumberText))
                        MessageBox.Show("ОШИБКА: пустое поле");

                    else
                    {
                        OnApply?.Invoke(" регистр. номер " + RegistrationNumberText
                            + " начало действия " + BeginOfActionDate.ToString().Substring(0, 10)
                            + " конец действия " + EndOfActionDate.ToString().Substring(0, 10)
                        );
                    }

                });
            }
        }

        /// <summary>
        /// Изменение
        /// </summary>
        public void Edit()
        {
            if (_tableService is ContractService service)
                service.Update(_row, SelectedIndexEmployees, SelectedIndexPaymentFrequencies,
                    RegistrationNumberText, BeginOfActionDate, EndOfActionDate,
                    AdditionalConditionsText, SelectedIndexFine);
        }
    }
}

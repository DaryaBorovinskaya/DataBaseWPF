using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public class AddPaymentVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private DateTime _dateOfPayment;
        private string _amountOfPaymentText;

        public Action<string> OnApply;

        public string AmountOfPaymentText
        {
            get { return _amountOfPaymentText; }
            set
            {
                Set(ref _amountOfPaymentText, value);
            }
        }

        public DateTime DateOfPayment
        {
            get { return _dateOfPayment; }
            set
            {
                Set(ref _dateOfPayment, value);
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


        public AddPaymentVM(ITableService tableService)
        {
            _tableService = tableService;

            _buttonContent = "Добавить";

            if (_tableService is ContractService service)
            {
                _windowTitle = $"Добавление в таблицу: {service.GetPaymentsTableName()}";
                
            }
            _dateOfPayment = DateTime.Now;
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


        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (string.IsNullOrEmpty(AmountOfPaymentText))
                        MessageBox.Show("ОШИБКА: пустое поле");


                    else
                    {
                        float value = CheckValuesFloat(AmountOfPaymentText);
                            
                        if (value != 0)
                        {
                            OnApply?.Invoke(" дата платежа " + DateOfPayment.Date.ToString().Substring(0,10) +  " сумма " + AmountOfPaymentText);
                        }
                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is ContractService service)
                service.AddPayment(DateOfPayment, float.Parse(AmountOfPaymentText));
            DateOfPayment = DateTime.Now;
            AmountOfPaymentText = string.Empty;
        }
    }
}

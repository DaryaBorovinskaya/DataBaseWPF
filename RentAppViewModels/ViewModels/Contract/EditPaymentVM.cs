using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public class EditPaymentVM : BasicVM
    {
        private DataRow _row;
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

        public EditPaymentVM(DataRow row, ITableService tableService)
        {
            _row = row;
            _tableService = tableService;

            _buttonContent = "Внести изменения";

            if (_tableService is ContractService service)
            {
                _windowTitle = $"Изменение данных таблицы: {service.GetPaymentsTableName()}";
                _dateOfPayment = DateTime.Parse(row[0].ToString());
                _amountOfPaymentText = row[1].ToString();
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
                            OnApply?.Invoke(" дата платежа " + DateOfPayment.Date.ToString().Substring(0, 10) + " сумма " + AmountOfPaymentText);
                        }
                    }

                });
            }
        }

        public void Edit()
        {
            if (_tableService is ContractService service)
                service.UpdatePayment(_row, DateOfPayment, float.Parse(AmountOfPaymentText));
            
        }
    }
}

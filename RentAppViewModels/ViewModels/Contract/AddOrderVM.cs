using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public  class AddOrderVM : BasicVM
    {
        private ITableService _tableService;
        private string _windowTitle;
        private string _buttonContent;
        private DateTime _beginOfRentDate;
        private DateTime _endOfRentDate;
        private string _rentalPaymentText;

        private List<string> _premisesComboBox;
        private int _selectedIndexPremises;
        private List<string> _rentalPurposesComboBox;
        private int _selectedIndexRentalPurposes;

        public Action<string> OnApply;




        public List<string> PremisesComboBox
        {
            get { return _premisesComboBox; }
            set
            {
                Set(ref _premisesComboBox, value);
            }
        }

        public int SelectedIndexPremises
        {
            get { return _selectedIndexPremises; }
            set
            {
                Set(ref _selectedIndexPremises, value);
            }
        }


        public List<string> RentalPurposesComboBox
        {
            get { return _rentalPurposesComboBox; }
            set
            {
                Set(ref _rentalPurposesComboBox, value);
            }
        }

        public int SelectedIndexRentalPurposes
        {
            get { return _selectedIndexRentalPurposes; }
            set
            {
                Set(ref _selectedIndexRentalPurposes, value);
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


        public DateTime BeginOfRentDate
        {
            get { return _beginOfRentDate; }
            set
            {
                Set(ref _beginOfRentDate, value);
            }
        }


        public DateTime EndOfRentDate
        {
            get { return _endOfRentDate; }
            set
            {
                Set(ref _endOfRentDate, value);
            }
        }

        public string RentalPaymentText
        {
            get { return _rentalPaymentText; }
            set
            {
                Set(ref _rentalPaymentText, value);
            }
        }



        public AddOrderVM(ITableService tableService)
        {
            _tableService = tableService;

            _buttonContent = "Добавить";

            if (_tableService is ContractService service)
            {
                _windowTitle = $"Добавление в таблицу: {service.GetOrdersTableName()}";
                _premisesComboBox = service.GetOrderPremises();
                _rentalPurposesComboBox = service.GetOrdersRentalPurposes();
            }

            _beginOfRentDate = DateTime.Now;
            _endOfRentDate = DateTime.Now;
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
                    if (SelectedIndexPremises == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение помещения");
                    else if (SelectedIndexRentalPurposes == -1)
                        MessageBox.Show("ОШИБКА: не выбрано значение цели аренды");
                    else if (string.IsNullOrEmpty(RentalPaymentText))
                        MessageBox.Show("ОШИБКА: пустое поле");


                    else
                    {
                        float value = CheckValuesFloat(RentalPaymentText);

                        if (value != 0 )
                        {
                            OnApply?.Invoke(PremisesComboBox[SelectedIndexPremises] 
                                    + " цель аренды " + RentalPurposesComboBox[SelectedIndexRentalPurposes]
                                    );
                        }

                    }
                });
            }
        }

        public void Add()
        {
            if (_tableService is ContractService service)
            {
                service.AddOrder(SelectedIndexPremises, SelectedIndexRentalPurposes, BeginOfRentDate,
                    EndOfRentDate, float.Parse(RentalPaymentText));
                PremisesComboBox = service.GetOrderPremises();
            }
            SelectedIndexPremises = -1;
            SelectedIndexRentalPurposes = -1;
            RentalPaymentText = string.Empty;
            BeginOfRentDate = DateTime.Now;
            EndOfRentDate = DateTime.Now;
        }
    }
}

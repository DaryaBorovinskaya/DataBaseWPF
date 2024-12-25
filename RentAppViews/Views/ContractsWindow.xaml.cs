using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Contract;
using DataBase1WPF.Views.AddOrEdit;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ContractsWindow.xaml
    /// </summary>
    public partial class ContractsWindow : Window
    {
        private ITableService _tableService;
        public ContractsWindow(DataRow row, ITableService clientService, ITableService tableService, uint client_id)
        {
            InitializeComponent();
            _tableService = tableService;
            DataContext = new ContractVM(row, clientService, tableService, client_id);
            if (DataContext is ContractVM contractVM)
            {
                contractVM.OnAdd += Add;
                contractVM.OnEdit += Edit;
                contractVM.OnDelete += Delete;

                contractVM.OnAddOrders += AddOrder;
                contractVM.OnEditOrders += EditOrder;
                contractVM.OnDeleteOrders += DeleteOrder;

                contractVM.OnAddPayments += AddPayment;
                contractVM.OnEditPayments += EditPayment;
                contractVM.OnDeletePayments += DeletePayment;
            }
        }


        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService)
        {
            AddOrEditContractWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditContractWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Удаление 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, " регистр. номер " + row[0].ToString() + 
                " начало действия " + row[1].ToString() + " конец действия " + row[2].ToString());
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Добавление заказа
        /// </summary>
        /// <param name="tableService"></param>
        public void AddOrder(ITableService tableService)
        {
            AddOrEditOrderWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение заказа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void EditOrder(DataRow row, ITableService tableService)
        {
            AddOrEditOrderWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Удаление заказа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void DeleteOrder(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString()
                + " , " + row[1] + " , " + row[2].ToString() 
                + " , этаж " + row[3].ToString() 
                + " , номер " + row[4].ToString()
                + " , площадь " + row[5].ToString() + " кв. м. "
                + ", цель аренды " + row[6].ToString(),
                OtherTablesEnum.Orders);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Добавление платежа
        /// </summary>
        /// <param name="tableService"></param>
        public void AddPayment(ITableService tableService)
        {
            AddOrEditPaymentWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение платежа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void EditPayment(DataRow row, ITableService tableService)
        {
            AddOrEditPaymentWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Удаление платежа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void DeletePayment(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, " дата  платежа " + row[0].ToString() +
                " сумма " + row[1].ToString() ,
                OtherTablesEnum.Payments);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ContractVM contractVM)
                contractVM.DataTableMouseDown();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ContractVM contractVM)
                contractVM.DataTableMouseLeave();
        }

        
    }
}

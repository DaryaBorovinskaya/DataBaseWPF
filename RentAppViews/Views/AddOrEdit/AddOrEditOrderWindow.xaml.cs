using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Contract;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditOrderWindow.xaml
    /// </summary>
    public partial class AddOrEditOrderWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditOrderWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add 
                ? new AddOrderVM(tableService) 
                : new EditOrderVM(row, tableService);

            if (DataContext is AddOrderVM addOrderVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addOrderVM.OnApply += Apply;
            }
            if (DataContext is EditOrderVM editOrderVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editOrderVM.OnApply += Apply;
            }
        }



        /// <summary>
        /// Обработчик события Применить
        /// </summary>
        /// <param name="confirmText"></param>
        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is ContractVM contractVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                contractVM.UpdateDataTableOrders();
            }
            else if (_addOrEditWindow.DataContext is ContractVM contractVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                contractVM1.UpdateDataTableOrders();
                this.Close();
            }
        }
    }
}

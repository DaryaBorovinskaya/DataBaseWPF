using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Contract;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditContractWindow.xaml
    /// </summary>
    public partial class AddOrEditContractWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditContractWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add 
                ? new AddContractVM(tableService) 
                : new EditContractVM(row, tableService);

            if (DataContext is AddContractVM addContractVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addContractVM.OnApply += Apply;
            }
            if (DataContext is EditContractVM editContractVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editContractVM.OnApply += Apply;
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

                contractVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is ContractVM contractVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                contractVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

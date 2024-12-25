using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Handbooks;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditHandbookWindow.xaml
    /// </summary>
    public partial class AddOrEditHandbookWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditHandbookWindow(AddEditDeleteEnum addOrEdit,  ITableService tableService, Window window,
            DataRow row=null, int selectedIndex= 0)
        {
            InitializeComponent();
            
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddHandbookVM(tableService) : new EditHandbookVM(row, selectedIndex, tableService);
            if (DataContext is AddHandbookVM addHandbookVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addHandbookVM.OnApply += Apply;
            }
            if (DataContext is EditHandbookVM editHandbookVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editHandbookVM.OnApply += Apply;
            }
        }


        /// <summary>
        /// Обработчик события Применить
        /// </summary>
        /// <param name="confirmText"></param>
        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM 
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                handbooksVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                handbooksVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

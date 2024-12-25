using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Employee;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditEmployeeWindow.xaml
    /// </summary>
    public partial class AddOrEditEmployeeWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditEmployeeWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddEmployeeVM(tableService) : new EditEmployeeVM(row, tableService);
            if (DataContext is AddEmployeeVM addEmployeeVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addEmployeeVM.OnApply += Apply;
            }
            if (DataContext is EditEmployeeVM editEmployeeVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editEmployeeVM.OnApply += Apply;
            }
        }


        /// <summary>
        /// Обработчик события Применить
        /// </summary>
        /// <param name="confirmText"></param>
        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is EmployeeVM employeeVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                employeeVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is EmployeeVM employeeVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                employeeVM1.UpdateDataTable();
                this.Close();
            }
        }

        
    }
}

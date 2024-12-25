using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Employee;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditWorkRecordCardWindow.xaml
    /// </summary>
    public partial class AddOrEditWorkRecordCardWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditWorkRecordCardWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddWorkRecordCardVM(tableService) : new EditWorkRecordCardVM(row, tableService);
            if (DataContext is AddWorkRecordCardVM addWorkRecordCardVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addWorkRecordCardVM.OnApply += Apply;
            }
            if (DataContext is EditWorkRecordCardVM editWorkRecordCardVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editWorkRecordCardVM.OnApply += Apply;
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

                employeeVM.UpdateDataTableWorkRecordCard();
            }
            else if (_addOrEditWindow.DataContext is EmployeeVM employeeVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                employeeVM1.UpdateDataTableWorkRecordCard();
                this.Close();
            }
        }
    }
}

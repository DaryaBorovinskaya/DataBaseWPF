using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.JuridicalPerson;
using DataBase1WPF.ViewModels.UserManagement;
using DataBase1WPF.ViewModels.UserManagementVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditUserManagementWindow.xaml
    /// </summary>
    public partial class AddOrEditUserManagementWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditUserManagementWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddUserManagementVM(tableService)
                : new EditUserManagementVM(row, tableService);

            if (DataContext is AddUserManagementVM addUserManagementVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addUserManagementVM.OnApply += Apply;
            }
            if (DataContext is EditUserManagementVM editUserManagementVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editUserManagementVM.OnApply += Apply;
            }
        }

        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is UserManagementVM userManagementVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                userManagementVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is UserManagementVM userManagementVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                userManagementVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

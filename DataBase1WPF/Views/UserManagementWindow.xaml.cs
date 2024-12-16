using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using DataBase1WPF.ViewModels.Handbooks;
using DataBase1WPF.ViewModels.UserManagementVM;
using DataBase1WPF.Views.AddOrEdit;
using Mysqlx.Crud;
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

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        public UserManagementWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new UserManagementVM(tableService, menuElemId);
            if (DataContext is UserManagementVM userManagementVM)
            {
                userManagementVM.OnAdd += Add;
                userManagementVM.OnEdit += Edit;
                userManagementVM.OnDelete += Delete;
            }
        }


        public void Add(ITableService tableService)
        {
            AddOrEditUserManagementWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditUserManagementWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, " пользователь " + row[0].ToString() 
                + " элемент меню " + row[1].ToString());
            window.ShowDialog();
        }



        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is UserManagementVM userManagementVM)
                userManagementVM.DataTableMouseDown();
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is UserManagementVM userManagementVM)
                userManagementVM.DataTableMouseLeave();
        }
    }
}

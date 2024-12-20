using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using DataBase1WPF.ViewModels.Employee;
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
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        public EmployeesWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new EmployeeVM(tableService, menuElemId);
            if (DataContext is EmployeeVM employeeVM)
            {
                employeeVM.OnAdd += Add;
                employeeVM.OnEdit += Edit;
                employeeVM.OnDelete += Delete;
                employeeVM.OnAddWorkRecordCard += AddWorkRecordCard;
                employeeVM.OnEditWorkRecordCard += EditWorkRecordCard;
                employeeVM.OnDeleteWorkRecordCard += DeleteWorkRecordCard;
            }
        }

        public void Add(ITableService tableService)
        {
            AddOrEditEmployeeWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditEmployeeWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[2].ToString() + " " + row[3].ToString() + " " + row[4].ToString());
            window.ShowDialog();
        }


        public void AddWorkRecordCard(ITableService tableService)
        {
            AddOrEditWorkRecordCardWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void EditWorkRecordCard(DataRow row, ITableService tableService)
        {
            AddOrEditWorkRecordCardWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void DeleteWorkRecordCard(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() , OtherTablesEnum.WorkRecordCard);
            window.ShowDialog();
        }
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableMouseDown();
        }

        private void DataGridWorkRecordCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableWordRecordCardMouseDown();
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableMouseLeave();
        }

        private void TextBoxWorkRecordCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableWordRecordCardMouseLeave();
        }
    }
}

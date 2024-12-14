using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Contract;
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
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, " регистр. номер " + row[0].ToString() + 
                " начало действия " + row[1].ToString() + " конец действия " + row[2].ToString());
            window.ShowDialog();
        }


        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ContractVM contractVM)
                contractVM.DataTableMouseDown();
        }

        

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ContractVM contractVM)
                contractVM.DataTableMouseLeave();
        }
    }
}

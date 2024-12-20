using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Individual;
using DataBase1WPF.ViewModels.JuridicalPerson;
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
    /// Логика взаимодействия для JuridicalPersonWindow.xaml
    /// </summary>
    public partial class JuridicalPersonWindow : Window
    {
        public JuridicalPersonWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new JuridicalPersonVM(tableService, menuElemId);
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
            {
                juridicalPersonVM.OnAdd += Add;
                juridicalPersonVM.OnEdit += Edit;
                juridicalPersonVM.OnDelete += Delete;
                juridicalPersonVM.OnContracts += Contracts;
            }
        }

        public void Add(ITableService tableService)
        {
            AddOrEditJuridicalPersonWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditJuridicalPersonWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this,
                row[0].ToString());
            window.ShowDialog();
        }


        public void Contracts(DataRow row, ITableService clientService, ITableService tableService,
            uint individual_id)
        {
            ContractsWindow window = new(row, clientService, tableService, individual_id);
            window.ShowDialog();
        }


        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
                juridicalPersonVM.DataTableMouseDown();
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
                juridicalPersonVM.DataTableMouseLeave();
        }



    }
}

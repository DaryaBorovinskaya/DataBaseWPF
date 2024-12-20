using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Individual;
using DataBase1WPF.Views.AddOrEdit;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для IndividualWindow.xaml
    /// </summary>
    public partial class IndividualWindow : Window
    {
        public IndividualWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new IndividualVM(tableService, menuElemId);
            if (DataContext is IndividualVM individualVM)
            {
                individualVM.OnAdd += Add;
                individualVM.OnEdit += Edit;
                individualVM.OnDelete += Delete;
                individualVM.OnContracts += Contracts;
            }
        }

        public void Add(ITableService tableService)
        {
            AddOrEditIndividualWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditIndividualWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, 
                row[0].ToString() + " " + row[1].ToString() + " " + row[2].ToString());
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
            if (DataContext is IndividualVM individualVM)
                individualVM.DataTableMouseDown();
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is IndividualVM individualVM)
                individualVM.DataTableMouseLeave();
        }
    }
}

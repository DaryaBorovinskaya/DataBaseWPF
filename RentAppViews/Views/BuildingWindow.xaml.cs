using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для BuildingWindow.xaml
    /// </summary>
    public partial class BuildingWindow : Window
    {
        public BuildingWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new BuildingVM(tableService, menuElemId);
            if (DataContext is BuildingVM buildingVM)
            {
                buildingVM.OnAdd += Add;
                buildingVM.OnEdit += Edit;
                buildingVM.OnDelete += Delete;
                buildingVM.OnAddPremise += AddPremise;
                buildingVM.OnEditPremise += EditPremise;
                buildingVM.OnDeletePremise += DeletePremise;
            }
        }

        public void Add(ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() + " " + row[1].ToString() + " " +row[2].ToString());
            window.ShowDialog();
        }


        public void AddPremise(ITableService tableService)
        {
            AddOrEditPremiseWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void EditPremise(DataRow row, ITableService tableService)
        {
            AddOrEditPremiseWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        public void DeletePremise(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() + " номер " + row[1].ToString() + " площадь " + row[2].ToString(), OtherTablesEnum.Premises);
            window.ShowDialog();
        }






        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseDown();
        }

        private void DataGridPremises_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTablePremisesMouseDown();
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseLeave();
        }

        private void TextBoxPremises_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseLeave();
        }
    }
}

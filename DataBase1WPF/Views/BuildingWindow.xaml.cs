using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using DataBase1WPF.ViewModels.Handbooks;
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
            }
        }

        public void Add(ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        public void Edit(DataRow row, int selectedIndex, ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row, selectedIndex);
            window.ShowDialog();
        }

        public void Delete(DataRow row, int selectedIndex, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() + " " + row[1].ToString() + " " +row[2].ToString());
            window.ShowDialog();
        }



        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseDown();
        }

        private void DataGrid_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseLeave();
        }
    }
}

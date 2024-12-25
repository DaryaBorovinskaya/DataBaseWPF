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


        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Изменение 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditBuildingWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Удаление 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() + " " + row[1].ToString() + " " +row[2].ToString());
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Добавление помещения
        /// </summary>
        /// <param name="tableService"></param>
        public void AddPremise(ITableService tableService)
        {
            AddOrEditPremiseWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Изменение помещения
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void EditPremise(DataRow row, ITableService tableService)
        {
            AddOrEditPremiseWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Удаление помещения
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void DeletePremise(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() + " номер " + row[1].ToString() + " площадь " + row[2].ToString(), OtherTablesEnum.Premises);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseDown();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент DataGridPremises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridPremises_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTablePremisesMouseDown();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseLeave();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBoxPremises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPremises_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BuildingVM buildingVM)
                buildingVM.DataTableMouseLeave();
        }
    }
}

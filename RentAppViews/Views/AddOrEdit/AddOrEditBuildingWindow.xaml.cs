using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditBuildingWindow.xaml
    /// </summary>
    public partial class AddOrEditBuildingWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditBuildingWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddBuildingVM(tableService) : new EditBuildingVM(row, tableService);
            if (DataContext is AddBuildingVM addBuildingVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addBuildingVM.OnApply += Apply;
            }
            if (DataContext is EditBuildingVM editBuildingVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editBuildingVM.OnApply += Apply;
            }
        }


        /// <summary>
        /// Обработчик события Применить
        /// </summary>
        /// <param name="confirmText"></param>
        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is BuildingVM buildingVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                buildingVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is BuildingVM buildingVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                buildingVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

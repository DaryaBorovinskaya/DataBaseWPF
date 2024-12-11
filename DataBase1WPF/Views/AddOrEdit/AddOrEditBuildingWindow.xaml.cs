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
    /// Логика взаимодействия для AddOrEditBuildingWindow.xaml
    /// </summary>
    public partial class AddOrEditBuildingWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditBuildingWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null, int selectedIndex = 0)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddBuildingVM(tableService) : new EditBuildingVM(row, selectedIndex, tableService);
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

using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Building;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditPremiseWindow.xaml
    /// </summary>
    public partial class AddOrEditPremiseWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditPremiseWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddPremiseVM(tableService) : new EditPremiseVM(row, tableService);
            if (DataContext is AddPremiseVM addPremiseVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addPremiseVM.OnApply += Apply;
            }
            if (DataContext is EditPremiseVM editPremiseVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editPremiseVM.OnApply += Apply;
            }

        }


        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is BuildingVM buildingVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                buildingVM.UpdateDataTablePremises();
            }
            else if (_addOrEditWindow.DataContext is BuildingVM buildingVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                buildingVM1.UpdateDataTablePremises();
                this.Close();
            }
        }
    }
}

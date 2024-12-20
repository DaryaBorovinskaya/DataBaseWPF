using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Employee;
using DataBase1WPF.ViewModels.Individual;
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

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditIndividualWindow.xaml
    /// </summary>
    public partial class AddOrEditIndividualWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditIndividualWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddIndividualVM(tableService)
                : new EditIndividualVM(row, tableService);
            if (DataContext is AddIndividualVM addIndividualVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addIndividualVM.OnApply += Apply;
            }
            if (DataContext is EditIndividualVM editIndividualVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editIndividualVM.OnApply += Apply;
            }
        }


        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is IndividualVM individualVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                individualVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is IndividualVM individualVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                individualVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

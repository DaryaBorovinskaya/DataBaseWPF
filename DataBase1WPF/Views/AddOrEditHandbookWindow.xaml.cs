using DataBase1WPF.Models.Services.Tables;
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
    /// Логика взаимодействия для AddOrEditHandbookWindow.xaml
    /// </summary>
    public partial class AddOrEditHandbookWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditHandbookWindow(AddEditDeleteEnum addOrEdit,  ITableService tableService, Window window,
            DataRow row = null, int selectedIndex= 0)
        {
            InitializeComponent();
            
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddHandbookVM(tableService) : new EditHandbookVM(row, selectedIndex, tableService);
            if (DataContext is AddHandbookVM addHandbookVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addHandbookVM.OnApply += Apply;
            }
            if (DataContext is EditHandbookVM editHandbookVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editHandbookVM.OnApply += Apply;
            }
        }

        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM 
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                DeleteWindow window = new(_addOrEdit, _tableService, this, null, confirmText);
                window.ShowDialog();

                handbooksVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                DeleteWindow window = new(_addOrEdit, _tableService, this, null, confirmText);
                window.ShowDialog();

                handbooksVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

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
        private AddOrEditEnum _addOrEdit;
        public AddOrEditHandbookWindow(AddOrEditEnum addOrEdit,  ITableService tableService, Window window,
            DataRow row = null, int selectedIndex= 0)
        {
            InitializeComponent();
            
            DataContext = addOrEdit == AddOrEditEnum.Add ? new AddHandbookVM(tableService) : new EditHandbookVM(row, selectedIndex, tableService);
            if (DataContext is AddHandbookVM addHandbookVM)
            {
                _addOrEditWindow = window;
                _addOrEdit = addOrEdit;
                addHandbookVM.OnApply += Apply;
            }
            if (DataContext is EditHandbookVM editHandbookVM)
            {
                _addOrEditWindow = window;
                _addOrEdit = addOrEdit;
                editHandbookVM.OnApply += Apply;
            }
        }

        private void Apply()
        {
            if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM 
                && _addOrEdit == AddOrEditEnum.Add)
            {
                handbooksVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is HandbooksVM handbooksVM1
                && _addOrEdit == AddOrEditEnum.Edit)
            {
                handbooksVM1.UpdateDataTable();
                this.Close();
            }
        }
    }
}

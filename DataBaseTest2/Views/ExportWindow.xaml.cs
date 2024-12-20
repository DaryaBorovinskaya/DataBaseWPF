using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
using DataBase1WPF.ViewModels.Building;
using DataBase1WPF.ViewModels.Handbooks;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ExportWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new ExportVM(tableService, menuElemId);
            
        }

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ExportVM exportVM)
                exportVM.DataTableMouseDown();
        }
    }
}

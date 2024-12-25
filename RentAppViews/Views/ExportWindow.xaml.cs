using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

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

        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ExportVM exportVM)
                exportVM.DataTableMouseDown();
        }
    }
}

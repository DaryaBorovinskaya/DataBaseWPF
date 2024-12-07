using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
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
    /// Логика взаимодействия для HandbooksWindow.xaml
    /// </summary>
    public partial class HandbooksWindow : Window
    {
        public HandbooksWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new HandbooksVM(tableService, menuElemId);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HandbooksVM handbooksVM)
                handbooksVM.DataTableMouseDown();
        }

        private void DataGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HandbooksVM handbooksVM)
                handbooksVM.DataTableMouseLeave();
        }
    }
}

using DataBase1WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class SQLqueryWindow : Window
    {
        public SQLqueryWindow()
        {
            InitializeComponent();
            DataContext = new SQLqueryVM();
           
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
        }

        
    }
}

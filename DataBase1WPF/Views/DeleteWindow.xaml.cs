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
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow(DataRow row, int selectedIndex, ITableService tableService)
        {
            InitializeComponent();
            DataContext = new DeleteHandbookVM(row, selectedIndex, tableService);
        }
    }
}

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
        public AddOrEditHandbookWindow(AddOrEditEnum addOrEdit,  ITableService tableService, DataRow row = null, int selectedIndex= 0)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddOrEditEnum.Add ? new AddHandbookVM(tableService) : new EditHandbookVM(row, selectedIndex, tableService);
        }
    }
}

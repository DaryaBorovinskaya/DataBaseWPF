using DataBase1WPF.Models.Services.Tables;
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
    /// Логика взаимодействия для AddOrEditPremiseWindow.xaml
    /// </summary>
    public partial class AddOrEditPremiseWindow : Window
    {
        public AddOrEditPremiseWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null, int selectedIndex = 0)
        {
            InitializeComponent();
            
        }
    }
}

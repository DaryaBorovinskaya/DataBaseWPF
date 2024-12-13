using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Contract;
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
    /// Логика взаимодействия для ContractsWindow.xaml
    /// </summary>
    public partial class ContractsWindow : Window
    {
        private ITableService _tableService;
        public ContractsWindow(DataRow row, ITableService clientService, ITableService tableService, uint client_id)
        {
            InitializeComponent();
            _tableService = tableService;
            DataContext = new ContractVM(row, clientService, tableService, client_id);
        }
    }
}

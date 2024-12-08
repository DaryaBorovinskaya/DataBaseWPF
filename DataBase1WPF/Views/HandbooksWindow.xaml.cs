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
    /// Логика взаимодействия для HandbooksWindow.xaml
    /// </summary>
    public partial class HandbooksWindow : Window
    {
        public HandbooksWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new HandbooksVM(tableService, menuElemId);
            if (DataContext is HandbooksVM handbooksVM)
            {
                handbooksVM.OnAdd += AddHandbook;
                handbooksVM.OnEdit += EditHandbook;
                handbooksVM.OnDelete += DeleteHandbook;
            }
        }

        public void AddHandbook(ITableService tableService) 
        {
            AddOrEditHandbookWindow window = new(AddOrEditEnum.Add, tableService);
            window.Show();
        }

        public void EditHandbook(DataRow row, int selectedIndex, ITableService tableService)
        {
            AddOrEditHandbookWindow window = new(AddOrEditEnum.Edit, tableService, row, selectedIndex);
            window.Show();
        }

        public void DeleteHandbook(DataRow row, int selectedIndex, ITableService tableService)
        {
            DeleteWindow window = new(row, selectedIndex,  tableService, this );
            window.Show();
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

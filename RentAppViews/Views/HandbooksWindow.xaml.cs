using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Handbooks;
using System.Data;
using System.Windows;
using System.Windows.Input;

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
                handbooksVM.OnAdd += Add;
                handbooksVM.OnEdit += Edit;
                handbooksVM.OnDelete += Delete;
            }
        }


        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService) 
        {
            AddOrEditHandbookWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, int selectedIndex, ITableService tableService)
        {
            AddOrEditHandbookWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row, selectedIndex);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Удаление 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, int selectedIndex, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete ,tableService, this, row[0].ToString() );
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HandbooksVM handbooksVM)
                handbooksVM.DataTableMouseDown();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HandbooksVM handbooksVM)
                handbooksVM.DataTableMouseLeave();
        }

        
    }
}

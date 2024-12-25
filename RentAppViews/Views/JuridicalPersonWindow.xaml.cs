using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.JuridicalPerson;
using DataBase1WPF.Views.AddOrEdit;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для JuridicalPersonWindow.xaml
    /// </summary>
    public partial class JuridicalPersonWindow : Window
    {
        public JuridicalPersonWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new JuridicalPersonVM(tableService, menuElemId);
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
            {
                juridicalPersonVM.OnAdd += Add;
                juridicalPersonVM.OnEdit += Edit;
                juridicalPersonVM.OnDelete += Delete;
                juridicalPersonVM.OnContracts += Contracts;
            }
        }

        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService)
        {
            AddOrEditJuridicalPersonWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Изменение
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditJuridicalPersonWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Удаление
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this,
                row[0].ToString());
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Договоры
        /// </summary>
        /// <param name="row"></param>
        /// <param name="clientService"></param>
        /// <param name="tableService"></param>
        /// <param name="individual_id"></param>
        public void Contracts(DataRow row, ITableService clientService, ITableService tableService,
            uint individual_id)
        {
            ContractsWindow window = new(row, clientService, tableService, individual_id);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
                juridicalPersonVM.DataTableMouseDown();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is JuridicalPersonVM juridicalPersonVM)
                juridicalPersonVM.DataTableMouseLeave();
        }



    }
}

using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Employee;
using DataBase1WPF.Views.AddOrEdit;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        public EmployeesWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new EmployeeVM(tableService, menuElemId);
            if (DataContext is EmployeeVM employeeVM)
            {
                employeeVM.OnAdd += Add;
                employeeVM.OnEdit += Edit;
                employeeVM.OnDelete += Delete;
                employeeVM.OnAddWorkRecordCard += AddWorkRecordCard;
                employeeVM.OnEditWorkRecordCard += EditWorkRecordCard;
                employeeVM.OnDeleteWorkRecordCard += DeleteWorkRecordCard;
            }
        }

        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService)
        {
            AddOrEditEmployeeWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditEmployeeWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Удаление 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[2].ToString() + " " + row[3].ToString() + " " + row[4].ToString());
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Добавление записи в трудовую книжку
        /// </summary>
        /// <param name="tableService"></param>
        public void AddWorkRecordCard(ITableService tableService)
        {
            AddOrEditWorkRecordCardWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Изменение записи из трудовой книжки
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void EditWorkRecordCard(DataRow row, ITableService tableService)
        {
            AddOrEditWorkRecordCardWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Удаление записи из трудовой книжки 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void DeleteWorkRecordCard(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, row[0].ToString() , OtherTablesEnum.WorkRecordCard);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableMouseDown();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент DataGridWorkRecordCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridWorkRecordCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableWordRecordCardMouseDown();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableMouseLeave();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент TextBoxWorkRecordCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWorkRecordCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EmployeeVM employeeVM)
                employeeVM.DataTableWordRecordCardMouseLeave();
        }
    }
}

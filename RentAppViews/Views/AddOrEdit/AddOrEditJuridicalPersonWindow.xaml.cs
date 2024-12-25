using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.JuridicalPerson;
using System.Data;
using System.Windows;

namespace DataBase1WPF.Views.AddOrEdit
{
    /// <summary>
    /// Логика взаимодействия для AddJuridicalPersonWindow.xaml
    /// </summary>
    public partial class AddOrEditJuridicalPersonWindow : Window
    {
        private Window _addOrEditWindow;
        private AddEditDeleteEnum _addOrEdit;
        private ITableService _tableService;
        public AddOrEditJuridicalPersonWindow(AddEditDeleteEnum addOrEdit, ITableService tableService, Window window,
            DataRow row = null)
        {
            InitializeComponent();
            DataContext = addOrEdit == AddEditDeleteEnum.Add ? new AddJuridicalPersonVM(tableService)
                : new EditJuridicalPersonVM(row, tableService);

            if (DataContext is AddJuridicalPersonVM addJuridicalPersonVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                addJuridicalPersonVM.OnApply += Apply;
            }
            if (DataContext is EditJuridicalPersonVM editJuridicalPersonVM)
            {
                _addOrEditWindow = window;
                _tableService = tableService;
                _addOrEdit = addOrEdit;
                editJuridicalPersonVM.OnApply += Apply;
            }
        }


        /// <summary>
        /// Обработчик события Применить
        /// </summary>
        /// <param name="confirmText"></param>
        private void Apply(string confirmText)
        {
            if (_addOrEditWindow.DataContext is JuridicalPersonVM juridicalPersonVM
                && _addOrEdit == AddEditDeleteEnum.Add)
            {

                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                juridicalPersonVM.UpdateDataTable();
            }
            else if (_addOrEditWindow.DataContext is JuridicalPersonVM juridicalPersonVM1
                && _addOrEdit == AddEditDeleteEnum.Edit)
            {
                ConfirmWindow window = new(_addOrEdit, _tableService, this, confirmText);
                window.ShowDialog();

                juridicalPersonVM1.UpdateDataTable();
                this.Close();
            }
        }


    }
}

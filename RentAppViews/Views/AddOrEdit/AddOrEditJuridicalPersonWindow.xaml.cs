using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Individual;
using DataBase1WPF.ViewModels.JuridicalPerson;
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

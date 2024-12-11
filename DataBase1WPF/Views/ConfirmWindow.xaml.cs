using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
using DataBase1WPF.ViewModels.Building;
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
    public partial class ConfirmWindow : Window
    {
        private Window _confirmWindow;
        private OtherTablesEnum _otherTables;
        public ConfirmWindow(AddEditDeleteEnum addEditDelete, ITableService tableService, Window window,  string confirmText=null,
            OtherTablesEnum otherTables = OtherTablesEnum.None)
        {

            InitializeComponent();  
            DataContext = new ConfirmVM(addEditDelete, tableService, confirmText);
            if (DataContext is ConfirmVM deleteVM)
            {
                _confirmWindow = window;
                _otherTables = otherTables;
                deleteVM.OnExit += Exit;
                deleteVM.OnApply += Apply;
            }
        }

        public void Apply()
        {
            if (_confirmWindow.DataContext is HandbooksVM handbooksVM)
            {
                handbooksVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddHandbookVM addHandbookVM)
            {
                addHandbookVM.Add();
            }
            else if (_confirmWindow.DataContext is EditHandbookVM editHandbookVM)
            {
                editHandbookVM.Edit();
            }

            else if (_confirmWindow.DataContext is BuildingVM buildingVM)
            {
                if (_otherTables == OtherTablesEnum.Premises)
                    buildingVM.DeletePremises();
                else
                    buildingVM.Delete();
            }
        }
        public void Exit()
        {
            Close();
        }
    }
}

using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.Building;

namespace DataBase1WPF.ViewModels.Employee
{
    public class EditEmployeeVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;
        private string _windowTitle;
        private string _buttonContent;

        public Action<string> OnApply;
        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }
        public EditEmployeeVM(DataRow row, ITableService tableService) 
        {
            _tableService = tableService;
            _row = row;
            _windowTitle = $"Изменение данных таблицы: {_tableService.GetTableName()}";
            _buttonContent = "Внести изменения";
        }

        public ICommand Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    

                });
            }
        }

        public void Edit()
        {
            
        }
    }
}

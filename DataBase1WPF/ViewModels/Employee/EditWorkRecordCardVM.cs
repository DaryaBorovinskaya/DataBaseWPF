using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using DataBase1WPF.Models.Services.Tables.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.ViewModels.Employee
{
    public class EditWorkRecordCardVM : BasicVM
    {
        private DataRow _row;
        private ITableService _tableService;
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

        public EditWorkRecordCardVM(DataRow row, ITableService tableService)
        {
            _row = row;
            _tableService = tableService;

            _buttonContent = "Внести изменения";

            if (_tableService is EmployeeService service)
            {
                _windowTitle = $"Изменение данных таблицы: {service.GetOtherTableName()}";
                
            }
        }
    }
}

using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Handbooks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBase1WPF.ViewModels.Handbooks
{
    public class EditHandbookVM : BasicVM
    {
        private string _windowTitle;
        private string _buttonContent;
        private Visibility _salaryVisibility;
        private string _title;
        private string _salary;
        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string ButtonContent
        {
            get { return _buttonContent; }
        }

        public Visibility SalaryVisibility
        {
            get { return _salaryVisibility; }
            set
            {
                Set(ref _salaryVisibility, value);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
            }
        }

        public string Salary
        {
            get { return _salary; }
            set
            {
                Set(ref _salary, value);
            }
        }
        public EditHandbookVM(DataRow row, int selectedIndex, ITableService tableService) 
        {
            _windowTitle = $"Изменение данных таблицы: {tableService.GetTableName()}";
            _buttonContent = "Внести изменения";
            _salaryVisibility = tableService.GetTableName() == (new PositionsService()).GetTableName()
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}

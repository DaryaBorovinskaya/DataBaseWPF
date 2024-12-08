using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace DataBase1WPF.ViewModels.Handbooks
{
    public class DeleteHandbookVM : BasicVM
    {
        private string _confirmLine;
        private ITableService _tableService;
        private int _selectedIndex;

        public Action OnExit;
        public Action OnApply;
        public string ConfirmLine
        {
            get { return _confirmLine; }
        }

        public DeleteHandbookVM(DataRow row, int selectedIndex, ITableService tableService) 
        {
            _confirmLine = $"Вы уверены, что хотите удалить {row[0]}?";
            _tableService = tableService;
            _selectedIndex = selectedIndex;
        }

        public ICommand ClickConfirm
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _tableService.Delete(_selectedIndex);
                    OnApply?.Invoke();
                    OnExit?.Invoke();
                });
            }
        }


        public ICommand ClickCancellation
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnExit?.Invoke();
                });
            }
        }
    }
}

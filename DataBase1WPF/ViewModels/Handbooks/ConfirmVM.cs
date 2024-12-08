using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Views;
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
    public class ConfirmVM : BasicVM
    {
        private string _confirmLine;
        private ITableService _tableService;
        private int _selectedIndex;
        private DataRow _row;

        public Action OnExit;
        public Action OnApply;
        public string ConfirmLine
        {
            get { return _confirmLine; }
        }

        public ConfirmVM(AddEditDeleteEnum addEditDelete, ITableService tableService, DataRow row=null, string confirmText=null) 
        {
            _row = row;
            if (row != null)
                _confirmLine = $"Вы уверены, что хотите удалить: {row[0]}?";
            else if (confirmText != null && addEditDelete == AddEditDeleteEnum.Add)
                _confirmLine = $"Вы уверены, что хотите добавить: {confirmText}?";
            else if (confirmText != null && addEditDelete == AddEditDeleteEnum.Edit)
                _confirmLine = $"Вы уверены, что хотите внести изменения: {confirmText}?";
            else
                _confirmLine = "Вы уверены?";
            _tableService = tableService;
        }

        public ICommand ClickConfirm
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
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

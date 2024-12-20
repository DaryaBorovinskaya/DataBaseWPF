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

namespace DataBase1WPF.ViewModels
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

        public ConfirmVM(AddEditDeleteEnum addEditDelete, ITableService tableService,  string confirmText = null)
        {
            if (confirmText != null)
            {
                if (addEditDelete == AddEditDeleteEnum.Add)
                    _confirmLine = $"Вы уверены, что хотите добавить: {confirmText}?";
                else if (addEditDelete == AddEditDeleteEnum.Edit)
                    _confirmLine = $"Вы уверены, что хотите внести изменения: {confirmText}?";
                else if (addEditDelete == AddEditDeleteEnum.Delete)
                    _confirmLine = $"Вы уверены, что хотите удалить: {confirmText}?";
            }
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

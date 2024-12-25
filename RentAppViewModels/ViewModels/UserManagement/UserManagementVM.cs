using DataBase1WPF.Models.Services.Tables;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.UserManagementVM
{
    /// <summary>
    /// Обработка и получение данных из окна UserManagementWindow
    /// </summary>
    public class UserManagementVM : BasicVM
    {
        private ITableService _tableService;
        private DataTable _dataTableUsers;
        private string _dataTableTitle;
        private string _searchDataInTable;
        private int _selectedIndex;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;

        


        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableUsers = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public DataTable DataTableUsers
        {
            get { return _dataTableUsers; }
            set
            {
                if (_dataTableUsers == null)
                    _dataTableUsers = value;
                else
                    Set(ref _dataTableUsers, value);
            }
        }

        public string DataTableTitle
        {
            get { return _dataTableTitle; }
            set
            {
                if (_dataTableTitle == null)
                    _dataTableTitle = value;
                else
                    Set(ref _dataTableTitle, value);
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
            }
        }

        public Visibility WriteVisibility
        {
            get { return _writeVisibility; }
            set
            {
                Set(ref _writeVisibility, value);

            }
        }

        public Visibility EditVisibility
        {
            get { return _editVisibility; }
            set
            {
                Set(ref _editVisibility, value);

            }
        }

        public Visibility DeleteVisibility
        {
            get { return _deleteVisibility; }
            set
            {
                Set(ref _deleteVisibility, value);

            }
        }


        public UserManagementVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            DataTableUsers = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _selectedIndex = -1;
        }


        /// <summary>
        /// Нажатие на DataTable
        /// </summary>
        public void DataTableMouseDown()
        {
            EditVisibility = _userAbilities.CanEdit ? Visibility.Visible : Visibility.Collapsed;
            DeleteVisibility = _userAbilities.CanDelete ? Visibility.Visible : Visibility.Collapsed;

        }


        /// <summary>
        /// Указатель мыши выходит из элемента DataTable
        /// </summary>
        public void DataTableMouseLeave()
        {
            EditVisibility = Visibility.Collapsed;
            DeleteVisibility = Visibility.Collapsed;

        }


       
        /// <summary>
        /// Нажатие на Добавить
        /// </summary>
        public ICommand ClickAdd
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAdd?.Invoke(_tableService);
                });
            }
        }

        /// <summary>
        /// Нажатие на Изменить
        /// </summary>
        public ICommand ClickEdit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableUsers.Rows.Count)
                        OnEdit?.Invoke(DataTableUsers.Rows[SelectedIndex], _tableService);
                });
            }
        }


        /// <summary>
        /// Нажатие на Удалить
        /// </summary>
        public ICommand ClickDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableUsers.Rows.Count)
                        OnDelete?.Invoke(DataTableUsers.Rows[SelectedIndex], _tableService);
                });
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        public void Delete()
        {
            _tableService.Delete(DataTableUsers.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        /// <summary>
        /// Обновление данных таблицы
        /// </summary>
        public void UpdateDataTable()
        {
            DataTableUsers = _tableService.GetValuesTable();
        }
    }
}

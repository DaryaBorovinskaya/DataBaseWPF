using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Employee;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Employee
{
    public class EmployeeVM : BasicVM
    {
        private ITableService _tableService;

        private DataTable _dataTableEmployees;
        private DataTable _dataTableWorkRecordCard;
        private Visibility _workRecordCardVisibility;
        private string _dataTableTitle;
        private string _dataTableOtherTitle;
        private string _searchDataInTable;
        private string _searchDataInTableWorkRecordCard;
        private int _selectedIndex;
        private int _selectedIndexWorkRecordCard;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow,  ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;

        public Action<ITableService> OnAddWorkRecordCard;
        public Action<DataRow,  ITableService> OnEditWorkRecordCard;
        public Action<DataRow,  ITableService> OnDeleteWorkRecordCard;
        public EmployeeVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            DataTableEmployees = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            if (_tableService is EmployeeService service)
                DataTableOtherTitle = service.GetOtherTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _workRecordCardVisibility = Visibility.Collapsed;
            _selectedIndex = -1;
        }

        public Visibility WorkRecordCardVisibility
        {
            get { return _workRecordCardVisibility; }
            set
            {
                Set(ref _workRecordCardVisibility, value);
            }
        }

        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableEmployees = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public string SearchDataInTableWorkRecordCard
        {
            get { return _searchDataInTableWorkRecordCard; }
            set
            {
                Set(ref _searchDataInTableWorkRecordCard, value);
                if (_tableService is EmployeeService service)
                    DataTableWorkRecordCard = service.SearchDataInTableWorkRecordCard(_searchDataInTableWorkRecordCard);
            }
        }


        public DataTable DataTableEmployees
        {
            get { return _dataTableEmployees; }
            set
            {
                if (_dataTableEmployees == null)
                    _dataTableEmployees = value;
                else
                    Set(ref _dataTableEmployees , value);
            }
        }

        public DataTable DataTableWorkRecordCard
        {
            get { return _dataTableWorkRecordCard; }
            set
            {
                Set(ref _dataTableWorkRecordCard, value);
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


        public string DataTableOtherTitle
        {
            get { return _dataTableOtherTitle; }
            set
            {
                if (_dataTableOtherTitle == null)
                    _dataTableOtherTitle = value;
                else
                    Set(ref _dataTableOtherTitle, value);
            }
        }


        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
                if (SelectedIndex >= 0 && SelectedIndex < DataTableEmployees.Rows.Count
                    && _tableService is EmployeeService service)
                {
                    DataTable? table = service.GetWorkRecordCardByEmployee(DataTableEmployees.Rows[SelectedIndex]);

                    WorkRecordCardVisibility = Visibility.Visible;
                    DataTableWorkRecordCard = table;
                }
            }
        }

        public int SelectedIndexWorkRecordCard
        {
            get { return _selectedIndexWorkRecordCard; }
            set
            {
                Set(ref _selectedIndexWorkRecordCard, value);
            }
        }


        public void DataTableMouseDown()
        {
            EditVisibility = _userAbilities.CanEdit ? Visibility.Visible : Visibility.Collapsed;
            DeleteVisibility = _userAbilities.CanDelete ? Visibility.Visible : Visibility.Collapsed;

        }

        public void DataTableWordRecordCardMouseDown()
        {
            EditVisibility = _userAbilities.CanEdit ? Visibility.Visible : Visibility.Collapsed;
            DeleteVisibility = _userAbilities.CanDelete ? Visibility.Visible : Visibility.Collapsed;
        }

        public void DataTableMouseLeave()
        {
            EditVisibility = Visibility.Collapsed;
            DeleteVisibility = Visibility.Collapsed;

        }

        public void DataTableWordRecordCardMouseLeave()
        {
            EditVisibility = Visibility.Collapsed;
            DeleteVisibility = Visibility.Collapsed;

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

        public ICommand ClickEdit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableEmployees.Rows.Count)
                        OnEdit?.Invoke(DataTableEmployees.Rows[SelectedIndex], _tableService);
                });
            }
        }



        public ICommand ClickDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableEmployees.Rows.Count)
                        OnDelete?.Invoke(DataTableEmployees.Rows[SelectedIndex], _tableService);
                });
            }
        }


        public ICommand ClickAddWorkRecordCard
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAddWorkRecordCard?.Invoke(_tableService);
                });
            }
        }

        public ICommand ClickEditWorkRecordCard
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexWorkRecordCard >= 0 && SelectedIndexWorkRecordCard < DataTableWorkRecordCard.Rows.Count)
                        OnEditWorkRecordCard?.Invoke(DataTableWorkRecordCard.Rows[SelectedIndexWorkRecordCard], _tableService);
                });
            }
        }



        public ICommand ClickDeleteWorkRecordCard
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexWorkRecordCard >= 0 && SelectedIndexWorkRecordCard < DataTableWorkRecordCard.Rows.Count)
                        OnDeleteWorkRecordCard?.Invoke(DataTableWorkRecordCard.Rows[SelectedIndexWorkRecordCard], _tableService);
                });
            }
        }




        public void Delete()
        {
            WorkRecordCardVisibility = Visibility.Collapsed;
            _tableService.Delete(DataTableEmployees.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        public void UpdateDataTable()
        {
            DataTableEmployees = _tableService.GetValuesTable();
        }

        public void DeleteWorkRecordCard()
        {
            if (_tableService is EmployeeService service)
                service.DeleteWorkRecordCard(DataTableWorkRecordCard.Rows[SelectedIndexWorkRecordCard]);
            UpdateDataTableWorkRecordCard();
        }

        public void UpdateDataTableWorkRecordCard()
        {
            if (_tableService is EmployeeService service)
                DataTableWorkRecordCard = service.GetWorkRecordCardByEmployee(DataTableEmployees.Rows[SelectedIndex]);
        }
    }
}

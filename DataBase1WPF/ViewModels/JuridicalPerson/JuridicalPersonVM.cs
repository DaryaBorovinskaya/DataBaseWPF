using DataBase1WPF.Models.Services.Tables.Contract;
using DataBase1WPF.Models.Services.Tables.Individual;
using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;

namespace DataBase1WPF.ViewModels.JuridicalPerson
{
    public class JuridicalPersonVM : BasicVM
    {
        private ITableService _tableService;
        private DataTable _dataTableJuridicalPersons;
        private string _dataTableTitle;
        private string _searchDataInTable;
        private int _selectedIndex;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private Visibility _contractVisibility;
        private UserAbilitiesType _userAbilities;

        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;
        public Action<DataRow, ITableService, ITableService, uint> OnContracts;


        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableJuridicalPersons = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public DataTable DataTableJuridicalPersons
        {
            get { return _dataTableJuridicalPersons; }
            set
            {
                Set(ref _dataTableJuridicalPersons, value);
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
                if (SelectedIndex >= 0 && SelectedIndex < DataTableJuridicalPersons.Rows.Count)
                    ContractVisibility = Visibility.Visible;

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

        public Visibility ContractVisibility
        {
            get { return _contractVisibility; }
            set
            {
                Set(ref _contractVisibility, value);
            }
        }



        public JuridicalPersonVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            _dataTableJuridicalPersons = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _contractVisibility = Visibility.Collapsed;
            _selectedIndex = -1;
        }

        public void DataTableMouseDown()
        {
            EditVisibility = _userAbilities.CanEdit ? Visibility.Visible : Visibility.Collapsed;
            DeleteVisibility = _userAbilities.CanDelete ? Visibility.Visible : Visibility.Collapsed;

        }

        public void DataTableMouseLeave()
        {
            EditVisibility = Visibility.Collapsed;
            DeleteVisibility = Visibility.Collapsed;

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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableJuridicalPersons.Rows.Count)
                        OnEdit?.Invoke(DataTableJuridicalPersons.Rows[SelectedIndex], _tableService);
                });
            }
        }



        public ICommand ClickDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableJuridicalPersons.Rows.Count)
                        OnDelete?.Invoke(DataTableJuridicalPersons.Rows[SelectedIndex], _tableService);
                });
            }
        }

        public ICommand ClickContracts
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableJuridicalPersons.Rows.Count
                    && _tableService is JuridicalPersonService service)
                        OnContracts?.Invoke(DataTableJuridicalPersons.Rows[SelectedIndex],
                            _tableService,
                            new ContractService(),
                            service.GetJuridicalPersonId(DataTableJuridicalPersons.Rows[SelectedIndex])
                            );
                });
            }
        }

        public void Delete()
        {
            _tableService.Delete(DataTableJuridicalPersons.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        public void UpdateDataTable()
        {
            DataTableJuridicalPersons = _tableService.GetValuesTable();
        }
    }
}

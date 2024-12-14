using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using DataBase1WPF.Models.Services.Tables.Employee;
using DataBase1WPF.Models.Services.Tables.Individual;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Contract
{
    public class ContractVM : BasicVM
    {
        private ITableService _tableService;
        private ITableService _clientService;
        private DataRow _row;
        private string _windowTitle;
        private DataTable _dataTableContracts;
        private string _dataTableTitle;
        private int _selectedIndex;
        private string _searchDataInTable;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;


        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public string DataTableTitle
        {
            get { return _dataTableTitle; }
            set
            {
                Set(ref _dataTableTitle, value);
            }
        }

        public DataTable DataTableContracts
        {
            get { return _dataTableContracts; }
            set
            {
                Set(ref _dataTableContracts, value);
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
                //if (SelectedIndex >= 0 && SelectedIndex < DataTableEmployees.Rows.Count
                //    && _tableService is EmployeeService service)
                //{
                //    DataTable? table = service.GetWorkRecordCardByEmployee(DataTableEmployees.Rows[SelectedIndex]);

                //    WorkRecordCardVisibility = Visibility.Visible;
                //    DataTableWorkRecordCard = table;
                //}
            }
        }

        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableContracts = _tableService.SearchDataInTable(_searchDataInTable);
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



        public ContractVM(DataRow row, ITableService clientService,
            ITableService tableService, uint client_id)
        {
            _tableService = tableService;
            _row = row;
            _clientService = clientService;
            _dataTableTitle = _tableService.GetTableName();
            if (_tableService is ContractService contractService)
            {
                if (_clientService is IndividualService service)
                {
                    _windowTitle = $"Договоры клиента: {row[0].ToString() + " "
                    + row[1].ToString() + " " + row[2].ToString() + " "}";
                    _dataTableContracts = contractService.GetValuesTableByIndividualId(client_id);
                    _userAbilities = service.UserAbilities;
                }
                else if (_clientService is JuridicalPersonService service1)
                {
                    _windowTitle = $"Договоры клиента: {row[0]}";
                    _dataTableContracts = contractService.GetValuesTableByJuridicalPersonId(client_id);
                    _userAbilities = service1.UserAbilities;
                }
            }

            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _selectedIndex = -1;
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableContracts.Rows.Count)
                        OnEdit?.Invoke(DataTableContracts.Rows[SelectedIndex], _tableService);
                });
            }
        }



        public ICommand ClickDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableContracts.Rows.Count)
                        OnDelete?.Invoke(DataTableContracts.Rows[SelectedIndex], _tableService);
                });
            }
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


        public void Delete()
        {
            //WorkRecordCardVisibility = Visibility.Collapsed;
            _tableService.Delete(DataTableContracts.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        public void UpdateDataTable()
        {
            DataTableContracts = _tableService.GetValuesTable();
        }
    }
}

using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
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
        private DataTable _dataTableOrders;
        private DataTable _dataTablePayments;

        private string _dataTableTitle;
        private string _dataTableOrdersTitle;
        private string _dataTablePaymentsTitle;

        private int _selectedIndex;
        private int _selectedIndexOrders;
        private int _selectedIndexPayments;

        private string _searchDataInTable;
        private string _searchDataInTableOrders;
        private string _searchDataInTablePayments;

        private Visibility _ordersVisibility;
        private Visibility _paymentsVisibility;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;

        public Action<ITableService> OnAddOrders;
        public Action<DataRow, ITableService> OnEditOrders;
        public Action<DataRow, ITableService> OnDeleteOrders;

        public Action<ITableService> OnAddPayments;
        public Action<DataRow, ITableService> OnEditPayments;
        public Action<DataRow, ITableService> OnDeletePayments;

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

        public string DataTableOrdersTitle
        {
            get { return _dataTableOrdersTitle; }
            set
            {
                Set(ref _dataTableOrdersTitle, value);
            }
        }

        public string DataTablePaymentsTitle
        {
            get { return _dataTablePaymentsTitle; }
            set
            {
                Set(ref _dataTablePaymentsTitle, value);
            }
        }


        public Visibility OrdersVisibility
        {
            get { return _ordersVisibility; }
            set
            {
                Set(ref _ordersVisibility, value);
            }
        }

        public Visibility PaymentsVisibility
        {
            get { return _paymentsVisibility; }
            set
            {
                Set(ref _paymentsVisibility, value);
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
        public DataTable DataTableOrders
        {
            get { return _dataTableOrders; }
            set
            {
                Set(ref _dataTableOrders, value);
            }
        }
        public DataTable DataTablePayments
        {
            get { return _dataTablePayments; }
            set
            {
                Set(ref _dataTablePayments, value);
            }
        }


        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
                if (SelectedIndex >= 0 && SelectedIndex < DataTableContracts.Rows.Count
                    && _tableService is ContractService service)
                {
                    DataTable? tableOrders = service.GetOrdersByContract(DataTableContracts.Rows[SelectedIndex]);
                    DataTable? tablePayments = service.GetPaymentsByContract(DataTableContracts.Rows[SelectedIndex]);
                    OrdersVisibility = Visibility.Visible;
                    PaymentsVisibility = Visibility.Visible;
                    DataTableOrders = tableOrders;
                    DataTablePayments = tablePayments;
                }
            }
        }
        public int SelectedIndexOrders
        {
            get { return _selectedIndexOrders; }
            set
            {
                Set(ref _selectedIndexOrders, value);
                
            }
        }

        public int SelectedIndexPayments
        {
            get { return _selectedIndexPayments; }
            set
            {
                Set(ref _selectedIndexPayments, value);
                
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
        public string SearchDataInTableOrders
        {
            get { return _searchDataInTableOrders; }
            set
            {
                Set(ref _searchDataInTableOrders, value);
                DataTableContracts = _tableService.SearchDataInTable(_searchDataInTableOrders);
            }
        }
        public string SearchDataInTablePayments
        {
            get { return _searchDataInTablePayments; }
            set
            {
                Set(ref _searchDataInTablePayments, value);
                DataTableContracts = _tableService.SearchDataInTable(_searchDataInTablePayments);
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
                _dataTableOrdersTitle = contractService.GetOrdersTableName();
                _dataTablePaymentsTitle = contractService.GetPaymentsTableName();
                
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


        public ICommand ClickAddOrders
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAddOrders?.Invoke(_tableService);
                });
            }
        }

        public ICommand ClickEditOrders
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexOrders >= 0 && SelectedIndexOrders < DataTableOrders.Rows.Count)
                        OnEditOrders?.Invoke(DataTableOrders.Rows[SelectedIndexOrders], _tableService);
                });
            }
        }

        public ICommand ClickDeleteOrders
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexOrders >= 0 && SelectedIndexOrders < DataTableOrders.Rows.Count)
                        OnDeleteOrders?.Invoke(DataTableOrders.Rows[SelectedIndexOrders], _tableService);
                });
            }
        }



        public ICommand ClickAddPayments
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAddPayments?.Invoke(_tableService);
                });
            }
        }

        public ICommand ClickEditPayments
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexPayments >= 0 && SelectedIndexPayments < DataTablePayments.Rows.Count)
                        OnEditPayments?.Invoke(DataTablePayments.Rows[SelectedIndexPayments], _tableService);
                });
            }
        }


        public ICommand ClickDeletePayments
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexPayments >= 0 && SelectedIndexPayments < DataTablePayments.Rows.Count)
                        OnDeletePayments?.Invoke(DataTablePayments.Rows[SelectedIndexPayments], _tableService);
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
            OrdersVisibility = Visibility.Collapsed;
            PaymentsVisibility = Visibility.Collapsed;
            _tableService.Delete(DataTableContracts.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        public void UpdateDataTable()
        {
            DataTableContracts = _tableService.GetValuesTable();
        }

        public void DeleteOrder()
        {
            if (_tableService is ContractService service)
                service.DeleteOrder(DataTableOrders.Rows[SelectedIndexOrders]);
            UpdateDataTableOrders();
        }

        public void UpdateDataTableOrders()
        {
            if (_tableService is ContractService service)
                DataTableOrders = service.GetOrdersByContract(DataTableContracts.Rows[SelectedIndex]);
        }

        public void DeletePayment()
        {
            if (_tableService is ContractService service)
                service.DeletePayment(DataTablePayments.Rows[SelectedIndexPayments]);
            UpdateDataTablePayments();
        }

        public void UpdateDataTablePayments()
        {
            if (_tableService is ContractService service)
                DataTablePayments = service.GetPaymentsByContract(DataTableContracts.Rows[SelectedIndex]);
        }

    }
}

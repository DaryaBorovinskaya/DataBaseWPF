using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using DataBase1WPF.Models.Services.Tables.Employee;
using DataBase1WPF.Models.Services.Tables.Individual;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Individual
{
    /// <summary>
    /// Обработка и получение данных из окна IndividualWindow
    /// </summary>
    public class IndividualVM : BasicVM
    {
        private ITableService _tableService;
        private DataTable _dataTableIndividuals;
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
                DataTableIndividuals = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public DataTable DataTableIndividuals
        {
            get { return _dataTableIndividuals; }
            set
            {
                if (_dataTableIndividuals == null)
                    _dataTableIndividuals = value;
                else
                    Set(ref _dataTableIndividuals, value);
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
                if (SelectedIndex >= 0 && SelectedIndex < DataTableIndividuals.Rows.Count)
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



        public IndividualVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            DataTableIndividuals = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _contractVisibility = Visibility.Collapsed;
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableIndividuals.Rows.Count)
                        OnEdit?.Invoke(DataTableIndividuals.Rows[SelectedIndex], _tableService);
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableIndividuals.Rows.Count)
                        OnDelete?.Invoke(DataTableIndividuals.Rows[SelectedIndex],  _tableService);
                });
            }
        }

        /// <summary>
        /// Нажатие на Договоры
        /// </summary>
        public ICommand ClickContracts
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableIndividuals.Rows.Count
                    && _tableService is IndividualService service)
                        OnContracts?.Invoke(DataTableIndividuals.Rows[SelectedIndex], 
                            _tableService,
                            new ContractService(), 
                            service.GetIndividualId(DataTableIndividuals.Rows[SelectedIndex])
                            );
                });
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        public void Delete()
        {
            _tableService.Delete(DataTableIndividuals.Rows[SelectedIndex]);
            UpdateDataTable();
        }


        /// <summary>
        /// Обновление данных таблицы
        /// </summary>
        public void UpdateDataTable()
        {
            DataTableIndividuals = _tableService.GetValuesTable();
        }
    }
}

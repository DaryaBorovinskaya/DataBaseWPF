using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Building
{
    /// <summary>
    /// Обработка и получение данных из окна BuildingWindow
    /// </summary>
    public class BuildingVM : BasicVM
    {
        private ITableService _tableService;

        private DataTable _dataTableBuildings;
        private DataTable _dataTablePremises;
        private Visibility _premisesVisibility;
        private string _dataTableTitle;
        private string _dataTableOtherTitle;
        private string _searchDataInTable;
        private string _searchDataInTablePremises;
        private int _selectedIndex;
        private int _selectedIndexPremises;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow,  ITableService> OnDelete;

        public Action<ITableService> OnAddPremise;
        public Action<DataRow, ITableService> OnEditPremise;
        public Action<DataRow, ITableService> OnDeletePremise;

        public BuildingVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            DataTableBuildings = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            if (_tableService is BuildingService service)
                DataTableOtherTitle = service.GetOtherTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
            _premisesVisibility = Visibility.Collapsed;
            _selectedIndex = -1;
        }


        public Visibility PremisesVisibility
        {
            get { return _premisesVisibility; }
            set
            {
                Set(ref _premisesVisibility, value);
            }
        }

        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableBuildings = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public string SearchDataInTablePremises
        {
            get { return _searchDataInTablePremises; }
            set
            {
                Set(ref _searchDataInTablePremises, value);
                if (_tableService is BuildingService service)
                    DataTablePremises = service.SearchDataInTablePremises(_searchDataInTablePremises);
            }
        }


        public DataTable DataTableBuildings
        {
            get { return _dataTableBuildings; }
            set
            {
                if (_dataTableBuildings == null)
                    _dataTableBuildings = value;
                else
                    Set(ref _dataTableBuildings, value);
            }
        }

        public DataTable DataTablePremises
        {
            get { return _dataTablePremises; }
            set
            {
                Set(ref _dataTablePremises, value);
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
                if (SelectedIndex >= 0 && SelectedIndex < DataTableBuildings.Rows.Count 
                    && _tableService is BuildingService service)
                {
                    DataTable? table = service.GetPremisesByBuilding(DataTableBuildings.Rows[SelectedIndex]);

                    PremisesVisibility = Visibility.Visible;
                    DataTablePremises = table;
                }
            }
        }

        public int SelectedIndexPremises
        {
            get { return _selectedIndexPremises; }
            set
            {
                Set(ref _selectedIndexPremises, value);
            }
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
        /// Нажатие на DataTablePremises
        /// </summary>
        public void DataTablePremisesMouseDown()
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
        /// Указатель мыши выходит из элемента DataTablePremises
        /// </summary>
        public void DataTablePremisesMouseLeave()
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableBuildings.Rows.Count)
                        OnEdit?.Invoke(DataTableBuildings.Rows[SelectedIndex], _tableService);
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableBuildings.Rows.Count)
                        OnDelete?.Invoke(DataTableBuildings.Rows[SelectedIndex], _tableService);
                });
            }
        }

        /// <summary>
        /// Нажатие на Добавить (помещение)
        /// </summary>
        public ICommand ClickAddPremises
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAddPremise?.Invoke(_tableService);
                });
            }
        }

        /// <summary>
        /// Нажатие на Изменить (помещение)
        /// </summary>
        public ICommand ClickEditPremises
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexPremises >= 0 && SelectedIndexPremises < DataTablePremises.Rows.Count)
                        OnEditPremise?.Invoke(DataTablePremises.Rows[SelectedIndexPremises], _tableService);
                });
            }
        }


        /// <summary>
        /// Нажатие на Удалить (помещение)
        /// </summary>
        public ICommand ClickDeletePremises
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndexPremises >= 0 && SelectedIndexPremises < DataTablePremises.Rows.Count)
                        OnDeletePremise?.Invoke(DataTablePremises.Rows[SelectedIndexPremises], _tableService);
                });
            }
        }



        /// <summary>
        /// Удаление
        /// </summary>
        public void Delete()
        {
            PremisesVisibility = Visibility.Collapsed;
            _tableService.Delete(DataTableBuildings.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        /// <summary>
        /// Обновление данных таблицы
        /// </summary>
        public void UpdateDataTable()
        {
            DataTableBuildings = _tableService.GetValuesTable();
        }


        /// <summary>
        /// Удаление (помещение)
        /// </summary>
        public void DeletePremises()
        {
            if (_tableService is BuildingService service) 
                service.DeletePremises(DataTablePremises.Rows[SelectedIndexPremises]);
            UpdateDataTablePremises();
        }

        /// <summary>
        /// Обновление данных таблицы помещения
        /// </summary>
        public void UpdateDataTablePremises()
        {
            if (_tableService is BuildingService service)
                DataTablePremises = service.GetPremisesByBuilding(DataTableBuildings.Rows[SelectedIndex]);
        }
    }
}

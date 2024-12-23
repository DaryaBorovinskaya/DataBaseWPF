using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Contract;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    public class ExportVM : BasicVM
    {
        private ITableService _tableService;
        private DataTable _dataTableExport;
        private string _dataTableTitle;
        private int _selectedIndex;
        private Visibility _exportWordVisibility;
        private Visibility _exportExcelVisibility;
        private string _windowTitle;

        public Action<ITableService> OnAdd;
        public Action<DataRow, ITableService> OnEdit;
        public Action<DataRow, ITableService> OnDelete;



        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                Set(ref _windowTitle, value);
            }
        }

        

        public DataTable DataTableExport
        {
            get { return _dataTableExport; }
            set
            {
                if (_dataTableExport == null)
                    _dataTableExport = value;
                else
                    Set(ref _dataTableExport, value);
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

        public Visibility ExportWordVisibility
        {
            get { return _exportWordVisibility; }
            set
            {
                Set(ref _exportWordVisibility, value);

            }
        }

        public Visibility ExportExcelVisibility
        {
            get { return _exportExcelVisibility; }
            set
            {
                Set(ref _exportExcelVisibility, value);

            }
        }

        


        public ExportVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            _dataTableExport = _tableService.GetValuesTable();
            _dataTableTitle = _tableService.GetTableName();
            
            _exportWordVisibility =  Visibility.Collapsed ;
            _exportExcelVisibility = Visibility.Collapsed;
            _selectedIndex = -1;

            _windowTitle = $"Экспорт таблицы {_dataTableTitle}";
        }


        public ICommand ClickExportWord
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (_tableService is ContractService service)
                        service.ExportWord(DataTableExport.Rows[SelectedIndex]);
                });
            }
        }

        public ICommand ClickExportExcel
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (_tableService is ContractService service)
                        service.ExportExcel(DataTableExport);
                });
            }
        }

        public void DataTableMouseDown()
        {
            ExportWordVisibility =  Visibility.Visible;
            ExportExcelVisibility = Visibility.Visible;

        }


    }
}

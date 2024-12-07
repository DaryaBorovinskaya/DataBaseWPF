using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Handbooks;
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
    public class HandbooksVM : BasicVM
    {
        private ITableService _tableService;

        private DataTable _dataTableHandbooks;
        private string _dataTableTitle;
        private string _searchDataInTable;
        private int _selectedItemIndex;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public HandbooksVM(ITableService tableService, uint menuElemId)
        {
            _tableService = tableService;
            DataTableHandbooks = _tableService.GetValuesTable();
            DataTableTitle = _tableService.GetTableName();
            _userAbilities = _tableService.GetUserAbilities(menuElemId);
            _writeVisibility = _userAbilities.CanWrite? Visibility.Visible: Visibility.Collapsed;
            _editVisibility = Visibility.Collapsed;
            _deleteVisibility = Visibility.Collapsed;
        }



        public string SearchDataInTable
        {
            get { return _searchDataInTable; }
            set
            {
                Set(ref _searchDataInTable, value);
                DataTableHandbooks = _tableService.SearchDataInTable(_searchDataInTable);
            }
        }

        public DataTable DataTableHandbooks
        {
            get { return _dataTableHandbooks; }
            set
            {
                if (_dataTableHandbooks == null )
                    _dataTableHandbooks = value;
                else
                    Set(ref _dataTableHandbooks, value);
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

        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set 
            {
                Set(ref _selectedItemIndex, value);
                //WriteVisibility = _userAbilities.CanWrite ? Visibility.Visible : Visibility.Collapsed;
                //EditVisibility = _userAbilities.CanEdit ? Visibility.Visible : Visibility.Collapsed;
                //DeleteVisibility = _userAbilities.CanDelete ? Visibility.Visible : Visibility.Collapsed;
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
    }
}

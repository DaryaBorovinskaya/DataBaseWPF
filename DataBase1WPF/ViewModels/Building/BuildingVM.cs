﻿using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels.Building
{
    public class BuildingVM : BasicVM
    {
        private ITableService _tableService;

        private DataTable _dataTableBuildings;
        private DataTable _dataTablePremises;
        private Visibility _premisesVisibility;
        private string _dataTableTitle;
        private string _dataTableOtherTitle;
        private string _searchDataInTable;
        private int _selectedIndex;
        private Visibility _writeVisibility;
        private Visibility _editVisibility;
        private Visibility _deleteVisibility;
        private UserAbilitiesType _userAbilities;


        public Action<ITableService> OnAdd;
        public Action<DataRow, int, ITableService> OnEdit;
        public Action<DataRow, int, ITableService> OnDelete;

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
                    if (table != null && table.Rows.Count != 0)
                    {
                        PremisesVisibility = Visibility.Visible;
                        DataTablePremises = table;
                    }
                    else
                    {
                        PremisesVisibility = Visibility.Collapsed;
                    }
                }
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
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableBuildings.Rows.Count)
                        OnEdit?.Invoke(DataTableBuildings.Rows[SelectedIndex], SelectedIndex, _tableService);
                });
            }
        }



        public ICommand ClickDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < DataTableBuildings.Rows.Count)
                        OnDelete?.Invoke(DataTableBuildings.Rows[SelectedIndex], SelectedIndex, _tableService);
                });
            }
        }

        public void Delete()
        {
            _tableService.Delete(DataTableBuildings.Rows[SelectedIndex]);
            UpdateDataTable();
        }

        public void UpdateDataTable()
        {
            DataTableBuildings = _tableService.GetValuesTable();
        }
    }
}
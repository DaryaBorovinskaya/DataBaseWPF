using DataBase1WPF.Models.Encryptors;
using DataBase1WPF.Models.Services.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data;
using DataBase1WPF.DataBase;
using System.Collections;
using DataBase1WPF.Models;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.Models.Services.Tables.Handbooks;

namespace DataBase1WPF.ViewModels
{
    public class MainVM : BasicVM
    {
        private DistrictsService _districtsService = new();
        private StreetsService _streetsService = new();
        private BanksService _banksService = new();
        private RentalPurposesService _rentalPurposesService = new();
        private PaymentFrequencyService _paymentFrequencyService = new();
        private TypesOfFinishingService _typesOfFinishingService = new();

        private DataTable _dataTableHandbooks = new();
        private string _dataTableTitle;

        public Action OnRegistration;
        public Action OnChangePassword;
        public Action OnSQLquery;

        public Action OnDistricts;
        public Action OnStreets;
        public Action OnBanks;
        public Action OnPositions;
        public Action OnPaymentFrequency;
        public Action OnRentalPurposes;
        public Action OnTypesOfFinishing;
        public Action OnFine;

        public ICommand ClickRegistration
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnRegistration?.Invoke();
                });
            }
        }

        public ICommand ClickChangePassword
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnChangePassword?.Invoke();
                });
            }
        }

        public ICommand ClickSQLquery
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnSQLquery?.Invoke();
                });
            }
        }


        public ICommand ClickDistricts
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnDistricts?.Invoke();
                    DataTableHandbooks = _districtsService.GetValuesTable(_districtsService.GetValues());
                    DataTableTitle = _districtsService.GetTableName();
                });
            }
        }
        public ICommand ClickStreets
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnStreets?.Invoke();
                    DataTableHandbooks = _streetsService.GetValuesTable(_streetsService.GetValues());
                    DataTableTitle = _streetsService.GetTableName();
                });
            }
        }
        public ICommand ClickBanks
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnBanks?.Invoke();
                    DataTableHandbooks = _banksService.GetValuesTable(_banksService.GetValues());
                    DataTableTitle = _banksService.GetTableName();
                });
            }
        }
        public ICommand ClickPositions
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPositions?.Invoke();
                });
            }
        }
        public ICommand ClickPaymentFrequency
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPaymentFrequency?.Invoke();
                    DataTableHandbooks = _paymentFrequencyService.GetValuesTable(_paymentFrequencyService.GetValues());
                    DataTableTitle = _paymentFrequencyService.GetTableName();
                });
            }
        }
        public ICommand ClickRentalPurposes
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnRentalPurposes?.Invoke();
                    DataTableHandbooks = _rentalPurposesService.GetValuesTable(_rentalPurposesService.GetValues());
                    DataTableTitle = _rentalPurposesService.GetTableName();
                });
            }
        }
        public ICommand ClickTypesOfFinishing
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnTypesOfFinishing?.Invoke();
                    DataTableHandbooks = _typesOfFinishingService.GetValuesTable(_typesOfFinishingService.GetValues());
                    DataTableTitle = _typesOfFinishingService.GetTableName();
                });
            }
        }
        public ICommand ClickFine
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnFine?.Invoke();
                });
            }
        }

         

        public DataTable DataTableHandbooks
        {
            get { return _dataTableHandbooks; }
            set
            {
                Set(ref _dataTableHandbooks, value);
            }
        }

        public string DataTableTitle
        {
            get { return _dataTableTitle; }
            set
            {
                Set(ref _dataTableTitle, value);
            }
        }
    }
}

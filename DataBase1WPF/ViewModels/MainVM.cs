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
using DataBase1WPF.Models.Services.Tables;

namespace DataBase1WPF.ViewModels
{
    public class MainVM : BasicVM
    {

        public Action OnRegistration;
        public Action OnChangePassword;
        public Action OnSQLquery;

        public Action<ITableService> OnDistricts;
        public Action<ITableService> OnStreets;
        public Action<ITableService> OnBanks;
        public Action<ITableService> OnPositions;
        public Action<ITableService> OnPaymentFrequency;
        public Action<ITableService> OnRentalPurposes;
        public Action<ITableService> OnTypesOfFinishing;
        public Action<ITableService> OnFine;

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
                    OnDistricts?.Invoke(new DistrictsService());
                });
            }
        }
        public ICommand ClickStreets
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnStreets?.Invoke(new StreetsService());
                });
            }
        }
        public ICommand ClickBanks
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnBanks?.Invoke(new BanksService());
                });
            }
        }
        public ICommand ClickPositions
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPositions?.Invoke(new PositionsService());
                });
            }
        }
        public ICommand ClickPaymentFrequency
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPaymentFrequency?.Invoke(new PaymentFrequencyService());
                });
            }
        }
        public ICommand ClickRentalPurposes
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnRentalPurposes?.Invoke(new RentalPurposesService());
                });
            }
        }
        public ICommand ClickTypesOfFinishing
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnTypesOfFinishing?.Invoke(new TypesOfFinishingService());
                });
            }
        }
        public ICommand ClickFine
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnFine?.Invoke(new FineService());
                });
            }
        }

         

        
    }
}

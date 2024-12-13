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
using DataBase1WPF.Models.Services.MenuElems;
using DataBase1WPF.Models.Services.Tables.Building;
using DataBase1WPF.Models.Services.Tables.Employee;
using DataBase1WPF.Models.Services.Tables.Individual;

namespace DataBase1WPF.ViewModels
{
    public class MainVM : BasicVM
    {
        private IMenuElemsService _menuElemsService = new MenuElemsService();

        public Action OnRegistration;
        public Action OnChangePassword;
        public Action OnSQLquery;

        public Action<ITableService, uint> OnDistricts;
        public Action<ITableService, uint> OnStreets;
        public Action<ITableService, uint> OnBanks;
        public Action<ITableService, uint> OnPositions;
        public Action<ITableService, uint> OnPaymentFrequency;
        public Action<ITableService, uint> OnRentalPurposes;
        public Action<ITableService, uint> OnTypesOfFinishing;
        public Action<ITableService, uint> OnFine;

        public Action<ITableService, uint> OnIndividuals;
        public Action<ITableService, uint> OnBuilding;
        public Action<ITableService, uint> OnEmployees;


        public Action OnAboutProgram;

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
                    OnDistricts?.Invoke(
                        new DistrictsService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickDistricts))
                        );
                });
            }
        }
        public ICommand ClickStreets
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnStreets?.Invoke(
                        new StreetsService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickStreets))
                        );
                });
            }
        }
        public ICommand ClickBanks
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnBanks?.Invoke(
                        new BanksService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickBanks))
                        );
                });
            }
        }
        public ICommand ClickPositions
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPositions?.Invoke(
                        new PositionsService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickPositions))
                        );
                });
            }
        }
        public ICommand ClickPaymentFrequency
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPaymentFrequency?.Invoke(
                        new PaymentFrequencyService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickPaymentFrequency))
                        );
                });
            }
        }
        public ICommand ClickRentalPurposes
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnRentalPurposes?.Invoke(
                        new RentalPurposesService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickRentalPurposes))
                        );
                });
            }
        }
        public ICommand ClickTypesOfFinishing
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnTypesOfFinishing?.Invoke(
                        new TypesOfFinishingService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickTypesOfFinishing))
                        );
                });
            }
        }
        public ICommand ClickFine
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnFine?.Invoke(
                        new FineService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickFine))
                        );
                });
            }
        }




        public ICommand ClickBuildings
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnBuilding?.Invoke(
                        new BuildingService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickBuildings))
                        );
                });
            }
        }

        public ICommand ClickAboutProgram
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnAboutProgram?.Invoke();
                });
            }
        }

        public ICommand ClickEmployees
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnEmployees?.Invoke(
                        new EmployeeService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickEmployees))
                        );
                });
            }
        }

        public ICommand ClickIndividuals
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnIndividuals?.Invoke(
                        new IndividualService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickIndividuals))
                        );
                });
            }
        }



    }
}

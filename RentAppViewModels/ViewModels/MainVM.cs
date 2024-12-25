using DataBase1WPF.Models.Services.MenuElems;
using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Building;
using DataBase1WPF.Models.Services.Tables.Contract;
using DataBase1WPF.Models.Services.Tables.Employee;
using DataBase1WPF.Models.Services.Tables.Handbooks;
using DataBase1WPF.Models.Services.Tables.Individual;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;
using DataBase1WPF.Models.Services.Tables.UserManagement;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    /// <summary>
    /// Обработка и получение данных из окна MainWindow
    /// </summary>
    public class MainVM : BasicVM
    {
        private IMenuElemsService _menuElemsService = new MenuElemsService();

        public Action OnRegistration;
        public Action<ITableService, uint> OnUserManagement;
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
        public Action<ITableService, uint> OnJuridicalPersons;
        public Action<ITableService, uint> OnBuilding;
        public Action<ITableService, uint> OnEmployees;

        public Action<ITableService, uint> OnExport;


        public Action OnAboutProgram;
        public Action OnContent;


        /// <summary>
        /// Обработка нажатия Регистрация
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Управление пользователями
        /// </summary>
        public ICommand ClickUserManagement
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnUserManagement?.Invoke(
                        new UserManagementService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickDistricts))
                        );
                });
            }
        }

        /// <summary>
        /// Обработка нажатия Смена пароля
        /// </summary>
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


        /// <summary>
        /// Обработка нажатия Запросы к базе данных
        /// </summary>
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


        /// <summary>
        /// Обработка нажатия Экспорт
        /// </summary>
        public ICommand ClickExport
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnExport?.Invoke(
                        new ContractService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickExport))
                        );
                });
            }
        }

        /// <summary>
        /// Обработка нажатия Районы
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Улицы
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Банки
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Должности
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Периодичность оплаты
        /// </summary>
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


        /// <summary>
        /// /// <summary>
        /// Обработка нажатия Цели аренды
        /// </summary>
        /// </summary>
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


        /// <summary>
        /// Обработка нажатия Виды отделки
        /// </summary>
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


        /// <summary>
        /// Обработка нажатия Штраф
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Здания
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия О программе
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Содержание
        /// </summary>
        public ICommand ClickContent
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnContent?.Invoke();
                });
            }
        }

        /// <summary>
        /// Обработка нажатия Сотрудники
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Физические лица
        /// </summary>
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

        /// <summary>
        /// Обработка нажатия Юридические лица
        /// </summary>
        public ICommand ClickJuridicalPersons
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnJuridicalPersons?.Invoke(
                        new JuridicalPersonService(), _menuElemsService.GetCurrentMenuElemId(nameof(ClickJuridicalPersons))
                        );
                });
            }
        }

    }
}

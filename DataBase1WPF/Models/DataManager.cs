using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Fine;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Individual;
using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Payment;
using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using DataBase1WPF.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models
{
    public class DataManager
    {
        private static DataManager _instance;

        public IUserDB CurrentUser {  get; set; }
        public IRepositoryDB<IUserDB> UserDB_Repository { get; set; }
        public IRepositoryDB<IMenuElemDB> MenuElemDB_Repository { get; set; }
        public IRepositoryDB<IUserAbilitiesDB> UserAbilitiesDB_Repository { get; set; }


        public IRepositoryDB<IHandbookDB> BankDB_Repository { get; set; }
        public IRepositoryDB<IBuildingDB> BuildingDB_Repository { get; set; }
        public IRepositoryDB<IContractDB> ContractDB_Repository { get; set; }
        public IRepositoryDB<IHandbookDB> DistrictDB_Repository { get; set; }
        public IRepositoryDB<IEmployeeDB> EmployeeDB_Repository { get; set; }
        public IRepositoryDB<IFineDB> FineDB_Repository { get; set; }
        public IRepositoryDB<IIndividualDB> IndividualDB_Repository { get; set; }
        public IRepositoryDB<IJuridicalPersonDB> JuridicalPersonDB_Repository { get; set; }
        public IRepositoryDB<IOrderDB> OrderDB_Repository { get; set; }
        public IRepositoryDB<IPaymentDB> PaymentDB_Repository { get; set; }
        public IRepositoryDB<IHandbookDB> PaymentFrequencyDB_Repository { get; set; }
        public IRepositoryDB<IPositionDB> PositionDB_Repository { get; set; }
        public IRepositoryDB<IPremiseDB> PremiseDB_Repository { get; set; }
        public IRepositoryDB<IHandbookDB> RentalPurposeDB_Repository { get; set; }
        public IRepositoryDB<IHandbookDB> StreetDB_Repository { get; set; }
        public IRepositoryDB<IHandbookDB> TypeOfFinishingDB_Repository { get; set; }
        public IRepositoryDB<IWorkRecordCardDB> WorkRecordCardDB_Repository { get; set; }


        private DataManager(IRepositoryDB<IUserDB> userDB_Repository, 
            IRepositoryDB<IMenuElemDB> menuElemDB_Repository, 
            IRepositoryDB<IUserAbilitiesDB> userAbilitiesDB_Repository, 
            IRepositoryDB<IHandbookDB> bankDB_Repository, IRepositoryDB<IBuildingDB> buildingDB_Repository, 
            IRepositoryDB<IContractDB> contractDB_Repository, IRepositoryDB<IHandbookDB> districtDB_Repository, 
            IRepositoryDB<IEmployeeDB> employeeDB_Repository, IRepositoryDB<IFineDB> fineDB_Repository, 
            IRepositoryDB<IIndividualDB> individualDB_Repository, 
            IRepositoryDB<IJuridicalPersonDB> juridicalPersonDB_Repository, IRepositoryDB<IOrderDB> orderDB_Repository, 
            IRepositoryDB<IPaymentDB> paymentDB_Repository, 
            IRepositoryDB<IHandbookDB> paymentFrequencyDB_Repository, IRepositoryDB<IPositionDB> positionDB_Repository, 
            IRepositoryDB<IPremiseDB> premiseDB_Repository, IRepositoryDB<IHandbookDB> rentalPurposeDB_Repository, 
            IRepositoryDB<IHandbookDB> streetDB_Repository, 
            IRepositoryDB<IHandbookDB> typeOfFinishingDB_Repository, 
            IRepositoryDB<IWorkRecordCardDB> workRecordCardDB_Repository)
        {
            CurrentUser = currentUser;
            UserDB_Repository = userDB_Repository;
            MenuElemDB_Repository = menuElemDB_Repository;
            UserAbilitiesDB_Repository = userAbilitiesDB_Repository;

            BankDB_Repository = bankDB_Repository;
            BuildingDB_Repository = buildingDB_Repository;
            ContractDB_Repository = contractDB_Repository;
            DistrictDB_Repository = districtDB_Repository;
            EmployeeDB_Repository = employeeDB_Repository;
            FineDB_Repository = fineDB_Repository;
            IndividualDB_Repository = individualDB_Repository;
            JuridicalPersonDB_Repository = juridicalPersonDB_Repository;
            OrderDB_Repository = orderDB_Repository;
            PaymentDB_Repository = paymentDB_Repository;
            PaymentFrequencyDB_Repository = paymentFrequencyDB_Repository;
            PositionDB_Repository = positionDB_Repository;
            PremiseDB_Repository = premiseDB_Repository;
            RentalPurposeDB_Repository = rentalPurposeDB_Repository;
            StreetDB_Repository = streetDB_Repository;
            TypeOfFinishingDB_Repository = typeOfFinishingDB_Repository;
            WorkRecordCardDB_Repository = workRecordCardDB_Repository;
        }

        public static DataManager GetInstance()
        {
            if (_instance == null)
                _instance = new DataManager(new UserDB_Repository(), new MenuElemDB_Repository(), 
                    new UserAbilitiesDB_Repository(), new BankDB_Repository(), new BuildingDB_Repository(),
                    new ContractDB_Repository(), new DistrictDB_Repository(), new EmployeeDB_Repository(),
                    new FineDB_Repository(), new IndividualDB_Repository(), new JuridicalPersonDB_Repository(),
                    new OrderDB_Repository(), new PaymentDB_Repository(), new PaymentFrequencyDB_Repository(),
                    new PositionDB_Repository(), new PremiseDB_Repository(), new RentalPurposeDB_Repository(),
                    new StreetDB_Repository(), new TypeOfFinishingDB_Repository(), new WorkRecordCardDB_Repository());
            return _instance;
        }
    }
}

using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using DataBase1WPF.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Order
{
    public class OrderService 
    {
        private Dictionary<DataRow, IOrderDB> _dataDictionary;
        private uint _contractId;
        private DataRow _rowForEdit;
        public DataTable? GetOrdersByContractId(uint id)
        {
            _contractId = id;
            DataTable table = null;
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                List<IOrderDB> values = repository.GetOrdersByContractId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[1]);
                table.Columns.Remove(table.Columns[2]);
                table.Columns.Remove(table.Columns[6]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }

        private List<IOrderDB> GetOrdersDBbyContractId(uint id)
        {
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                return repository.GetOrdersByContractId(id).ToList();
            }
            else return null;
        }
        public DataTable SearchDataInTable(uint buildingId, string searchLine)
        {
            DataTable table = new();
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                List<IOrderDB> values = repository.GetOrdersByContractId(buildingId).ToList().Where(
                    item => 
                       item.DistrictTitle.Contains(searchLine)
                    || item.StreetTitle.Contains(searchLine)
                    || item.HouseNumber.Contains(searchLine)
                    || item.FloorNumber.ToString().Contains(searchLine)
                    || item.PremiseNumber.Contains(searchLine)
                    || item.Area.ToString().Contains(searchLine)
                    || item.RentalPurposeTitle.Contains(searchLine)
                    || item.BeginOfRent.Contains(searchLine)
                    || item.EndOfRent.Contains(searchLine)
                    || item.RentalPayment.ToString().Contains(searchLine)).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[1]);
                table.Columns.Remove(table.Columns[2]);
                table.Columns.Remove(table.Columns[6]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);

            }
            return table;
        }


        


        public List<string> GetPremises()
        {
            List<string> premises = new();

            List<IBuildingDB> buildingsDB = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();
              
            List<IOrderDB> ordersDB = DataManager.GetInstance().OrderDB_Repository.Read().ToList(); ;

            foreach(IBuildingDB buildingDB in buildingsDB )
            {
                if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
                { 
                    List<IPremiseDB> premisesDB = repository.GetPremisesByBuildingId(buildingDB.Id).ToList();
                    
                    foreach (IPremiseDB premiseDB in premisesDB)
                    {
                        if (ordersDB.Find((item) => item.PremiseID == premiseDB.Id) == null)
                        {
                            if (premiseDB.BuildingID == buildingDB.Id)
                                premises.Add(buildingDB.DistrictTitle + " , "
                                + buildingDB.StreetTitle + " , "
                                + buildingDB.HouseNumber + " , этаж "
                                + premiseDB.FloorNumber + " , номер "
                                + premiseDB.PremiseNumber + " , "
                                + premiseDB.Area.ToString() + " кв. м.");
                            
                        }  

                        
                        
                    }
                }
                
            }

            return premises;
        }


        public List<IPremiseDB> GetPremisesDBForEdit(DataRow row)
        {
            List<IPremiseDB> premises = new();

            List<IBuildingDB> buildingsDB = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();

            List<IOrderDB> ordersDB = DataManager.GetInstance().OrderDB_Repository.Read().ToList(); ;

            foreach (IBuildingDB buildingDB in buildingsDB)
            {
                if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
                {
                    List<IPremiseDB> premisesDB = repository.GetPremisesByBuildingId(buildingDB.Id).ToList();

                    foreach (IPremiseDB premiseDB in premisesDB)
                    {
                        IOrderDB? foundOrder = ordersDB.Find((item) => item.PremiseID == premiseDB.Id);
                        if (foundOrder == null)
                        {
                            if (premiseDB.BuildingID == buildingDB.Id)
                                premises.Add(premiseDB);

                        }
                        else if (_dataDictionary[row].PremiseID == premiseDB.Id
                            && foundOrder.Id == _dataDictionary[row].Id)
                            premises.Add(premiseDB);


                    }
                }

            }

            return premises;
        }

        public List<string> GetPremisesForEdit(DataRow row)
        {
            _rowForEdit = row;

            List<string> premises = new();

            List<IBuildingDB> buildingsDB = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();

            List<IOrderDB> ordersDB = DataManager.GetInstance().OrderDB_Repository.Read().ToList(); ;

            foreach (IBuildingDB buildingDB in buildingsDB)
            {
                if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
                {
                    List<IPremiseDB> premisesDB = repository.GetPremisesByBuildingId(buildingDB.Id).ToList();

                    foreach (IPremiseDB premiseDB in premisesDB)
                    {
                        IOrderDB? foundOrder = ordersDB.Find((item) => item.PremiseID == premiseDB.Id);
                        if (foundOrder == null)
                        {
                            if (premiseDB.BuildingID == buildingDB.Id)
                                premises.Add(buildingDB.DistrictTitle + " , "
                                + buildingDB.StreetTitle + " , "
                                + buildingDB.HouseNumber + " , этаж "
                                + premiseDB.FloorNumber + " , номер "
                                + premiseDB.PremiseNumber + " , "
                                + premiseDB.Area.ToString() + " кв. м.");

                        }
                        else if ( _dataDictionary[row].PremiseID == premiseDB.Id
                            && foundOrder.Id == _dataDictionary[row].Id)
                            premises.Add(buildingDB.DistrictTitle + " , "
                                + buildingDB.StreetTitle + " , "
                                + buildingDB.HouseNumber + " , этаж "
                                + premiseDB.FloorNumber + " , номер "
                                + premiseDB.PremiseNumber + " , "
                                + premiseDB.Area.ToString() + " кв. м.");


                    }
                }

            }

            return premises;
        }

        private List<IPremiseDB> GetPremisesDB()
        {
            List<IPremiseDB> premises = new();

            List<IBuildingDB> buildingsDB = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();

            List<IOrderDB> ordersDB = DataManager.GetInstance().OrderDB_Repository.Read().ToList(); ;

            foreach (IBuildingDB buildingDB in buildingsDB)
            {
                if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
                {
                    List<IPremiseDB> premisesDB = repository.GetPremisesByBuildingId(buildingDB.Id).ToList();

                    foreach (IPremiseDB premiseDB in premisesDB)
                    {
                        if (ordersDB.Find((item) => item.PremiseID == premiseDB.Id) == null)
                        {
                            if (premiseDB.BuildingID == buildingDB.Id)
                                premises.Add(premiseDB);

                        }



                    }
                }

            }

            return premises;
        }


        public List<string> GetRentalPurposes()
        {
            List<string> rentalPurposes = new();

            List<IHandbookDB> rentalPurposesDB = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();

            foreach (IHandbookDB rentalPurposeDB in rentalPurposesDB)
                rentalPurposes.Add(rentalPurposeDB.Title);


            return rentalPurposes;
        }

        public int GetRentalPurposesSelectedIndex(DataRow row)
        {
            List<IHandbookDB> rentalPurposes = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();

            return rentalPurposes.FindIndex((elem) => elem.Id == _dataDictionary[row].RentalPurposeId);
        }


        public int GetPremisesSelectedIndex(DataRow row)
        {
            return GetPremisesDBForEdit(_rowForEdit).FindIndex((elem) => elem.Id == _dataDictionary[row].PremiseID);
        }


        public void Add(uint contractId, int premiseSelectedIndex, 
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            DataManager.GetInstance().OrderDB_Repository.Create(new OrderDB(
                contractId,
                GetPremisesDB()[premiseSelectedIndex].Id,
                DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList()[rentalPurposeSelectedIndex].Id,
                beginOfRent.ToString("yyyy-MM-dd"),
                endOfRent.ToString("yyyy-MM-dd"),
                rentalPayment
                ));
        }

        public void Update(DataRow row, uint contractId, int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            DataManager.GetInstance().OrderDB_Repository.Update(new OrderDB(
                _dataDictionary[row].Id,
                GetPremisesDBForEdit(_rowForEdit)[premiseSelectedIndex].Id,
                DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList()[rentalPurposeSelectedIndex].Id,
                contractId,
                beginOfRent.ToString("yyyy-MM-dd"),
                endOfRent.ToString("yyyy-MM-dd"),
                rentalPayment
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().OrderDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

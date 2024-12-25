using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Order
{
    /// <summary>
    /// Сервис для заказов
    /// </summary>
    public class OrderService 
    {
        private Dictionary<DataRow, IOrderDB> _dataDictionary;
        private uint _contractId;
        private DataRow _rowForEdit;

        /// <summary>
        /// Получение заказов в таблице DataTable по идентификатору договора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получение заказов (значения из базы данных) по идентификатору договора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<IOrderDB> GetOrdersDBbyContractId(uint id)
        {
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                return repository.GetOrdersByContractId(id).ToList();
            }
            else return null;
        }



        /// <summary>
        /// Поиск данных по таблице заказов
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
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


        

        /// <summary>
        /// Получение списка свободных помещений
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Получение помещений (значения из базы данных) при изменении помещения в заказе
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получение списка помещений при изменении помещения в заказе
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получение помещений (значения из базы данных)
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Получение списка целей аренды
        /// </summary>
        /// <returns></returns>
        public List<string> GetRentalPurposes()
        {
            List<string> rentalPurposes = new();

            List<IHandbookDB> rentalPurposesDB = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();

            foreach (IHandbookDB rentalPurposeDB in rentalPurposesDB)
                rentalPurposes.Add(rentalPurposeDB.Title);


            return rentalPurposes;
        }

        /// <summary>
        /// Получение индекса цели аренды у выбранного  заказа
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetRentalPurposesSelectedIndex(DataRow row)
        {
            List<IHandbookDB> rentalPurposes = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();

            return rentalPurposes.FindIndex((elem) => elem.Id == _dataDictionary[row].RentalPurposeId);
        }

        /// <summary>
        /// Получение индекса помещения у выбранного  заказа
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetPremisesSelectedIndex(DataRow row)
        {
            return GetPremisesDBForEdit(_rowForEdit).FindIndex((elem) => elem.Id == _dataDictionary[row].PremiseID);
        }


        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="premiseSelectedIndex"></param>
        /// <param name="rentalPurposeSelectedIndex"></param>
        /// <param name="beginOfRent"></param>
        /// <param name="endOfRent"></param>
        /// <param name="rentalPayment"></param>
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


        /// <summary>
        /// Изменение заказа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="contractId"></param>
        /// <param name="premiseSelectedIndex"></param>
        /// <param name="rentalPurposeSelectedIndex"></param>
        /// <param name="beginOfRent"></param>
        /// <param name="endOfRent"></param>
        /// <param name="rentalPayment"></param>
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

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().OrderDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

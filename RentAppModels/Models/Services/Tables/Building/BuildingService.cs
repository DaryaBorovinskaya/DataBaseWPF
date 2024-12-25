using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.Models.Services.Tables.Premise;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Building
{
    /// <summary>
    /// Сервис для зданий
    /// </summary>
    public class BuildingService : IBuildingService, ITableService
    {
        private IPremiseService _premiseService;
        private DataRow _selectedBuilding;
        
        private Dictionary<DataRow, IBuildingDB> _dataDictionary;

        /// <summary>
        /// Получение данных зданий
        /// </summary>
        /// <returns></returns>
        private List<IBuildingDB> GetValues()
        {
            List<IBuildingDB> values = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();
            return values;
        }

        public BuildingService()
        {
            _premiseService = new PremiseService();
        }

        /// <summary>
        /// Получение значений зданий в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IBuildingDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[2]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            
            return table;
        }

        /// <summary>
        /// Получение имени таблицы здания
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Здания";
        }

        /// <summary>
        /// Получение имени таблицы помещения
        /// </summary>
        /// <returns></returns>
        public string GetOtherTableName()
        {
            return "Помещения";
        }


        /// <summary>
        /// Поиск данных по таблице здания
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IBuildingDB> values = GetValues().Where(item => item.DistrictTitle.Contains(searchLine) 
            || item.StreetTitle.Contains(searchLine) || item.HouseNumber.Contains(searchLine)
            || item.NumberOfFloors.ToString().Contains(searchLine) || item.CountRentalPremises.ToString().Contains(searchLine)
            || item.CommandantPhoneNumber.Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[2]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение прав пользователя к зданиям
        /// </summary>
        /// <param name="menuElemId"></param>
        /// <returns></returns>
        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            UserAbilitiesType userAbilities = new();
            List<IUserAbilitiesDB> userAbilitiesDB = DataManager.GetInstance().UserAbilitiesDB_Repository.Read().ToList();


            foreach (IUserAbilitiesDB userAbilityDB in userAbilitiesDB)
            {
                if (userAbilityDB.UserId == DataManager.GetInstance().CurrentUser.Id
                    && userAbilityDB.MenuElemId == menuElemId)
                {
                    userAbilities.CanRead = userAbilityDB.R;
                    userAbilities.CanWrite = userAbilityDB.W;
                    userAbilities.CanEdit = userAbilityDB.E;
                    userAbilities.CanDelete = userAbilityDB.D;
                }
            }

            return userAbilities;
        }


        /// <summary>
        /// Получение списка районов
        /// </summary>
        /// <returns></returns>
        public List<string> GetDistricts()
        {
            List<string> districts = new();

            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            foreach(IHandbookDB districtDB in districtsDB) 
                districts.Add(districtDB.Title);
            

            return districts;
        }

        /// <summary>
        /// Получение списка улиц
        /// </summary>
        /// <returns></returns>
        public List<string> GetStreets()
        {
            List<string> streets = new();

            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            foreach (IHandbookDB streetDB in streetsDB)
                streets.Add(streetDB.Title);


            return streets;
        }



        /// <summary>
        /// Добавление нового здания
        /// </summary>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="houseNumber"></param>
        /// <param name="numberOfFloors"></param>
        /// <param name="countRentalPremises"></param>
        /// <param name="commandantPhoneNumber"></param>
        public void Add(int districtSelectedIndex, int streetSelectedIndex, 
            string houseNumber, uint numberOfFloors, uint countRentalPremises, 
            string commandantPhoneNumber)
        {
            DataManager.GetInstance().BuildingDB_Repository.Create(new BuildingDB(
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                houseNumber,
                numberOfFloors,
                countRentalPremises,
                commandantPhoneNumber
                ));
        }


        /// <summary>
        /// Получение индекса района у выбранного  здания
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetDistrictSelectedIndex(DataRow row)
        {
            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            return districtsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].DistrictId);
             
        }

        /// <summary>
        /// Получение индекса улицы у выбранного  здания
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetStreetsSelectedIndex(DataRow row)
        {
            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            return streetsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].StreetId);

        }

        /// <summary>
        /// Получение индекса вида отделки у выбранного значения здания
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetTypeOfFinishingSelectedIndex(DataRow row)
        {
            return _premiseService.GetTypeOfFinishingSelectedIndex(row);
        }


        /// <summary>
        /// Изменение данных здания
        /// </summary>
        /// <param name="row"></param>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="houseNumber"></param>
        /// <param name="numberOfFloors"></param>
        /// <param name="countRentalPremises"></param>
        /// <param name="commandantPhoneNumber"></param>
        public void Update(DataRow row, int districtSelectedIndex, int streetSelectedIndex,
            string houseNumber, uint numberOfFloors, uint countRentalPremises,
            string commandantPhoneNumber)
        {
            DataManager.GetInstance().BuildingDB_Repository.Update( new BuildingDB(
                _dataDictionary[row].Id,
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                houseNumber,
                numberOfFloors,
                countRentalPremises,
                commandantPhoneNumber
                ));
        }


        /// <summary>
        /// Удаление здания
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().BuildingDB_Repository.Delete(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение помещений по зданию
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataTable? GetPremisesByBuilding(DataRow row)
        {
            _selectedBuilding = row;
            return _premiseService.GetPremisesByBuilding(_dataDictionary[row].Id);
        }

        /// <summary>
        /// Получение частичных данных здания у выбранного помещения
        /// </summary>
        /// <returns></returns>
        public string GetSelectedBuildingText()
        {
            return _dataDictionary[_selectedBuilding].DistrictTitle + " "
                + _dataDictionary[_selectedBuilding].StreetTitle + " "
                + _dataDictionary[_selectedBuilding].HouseNumber;
        }

        /// <summary>
        /// Поиск данных по таблице помещений
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTablePremises(string searchLine)
        {
            return _premiseService.SearchDataInTable(_dataDictionary[_selectedBuilding].Id,searchLine);
        }


        /// <summary>
        /// Получение видов отделки
        /// </summary>
        /// <returns></returns>
        public List<string> GetTypesOfFinishing()
        {
           return _premiseService.GetTypesOfFinishing();
        }


        /// <summary>
        /// Добавление помещения
        /// </summary>
        /// <param name="typeOfFinishingIndex"></param>
        /// <param name="premiseNumber"></param>
        /// <param name="area"></param>
        /// <param name="floorNumber"></param>
        /// <param name="availAbilityOfPhoneNumber"></param>
        /// <param name="tempRentalPayment"></param>
        public void AddPremises(int typeOfFinishingIndex, string premiseNumber,
            float area, int floorNumber, bool availAbilityOfPhoneNumber,
            float tempRentalPayment)
        {
            _premiseService.Add(_dataDictionary[_selectedBuilding].Id,
                typeOfFinishingIndex, premiseNumber, area, floorNumber, availAbilityOfPhoneNumber,
                tempRentalPayment);
        }


        /// <summary>
        /// Изменение данных помещения
        /// </summary>
        /// <param name="row"></param>
        /// <param name="typeOfFinishingIndex"></param>
        /// <param name="premiseNumber"></param>
        /// <param name="area"></param>
        /// <param name="floorNumber"></param>
        /// <param name="availAbilityOfPhoneNumber"></param>
        /// <param name="tempRentalPayment"></param>
        public void UpdatePremises(DataRow row, int typeOfFinishingIndex, string premiseNumber,
            float area, int floorNumber, bool availAbilityOfPhoneNumber,
            float tempRentalPayment)
        {
            _premiseService.Update(row, _dataDictionary[_selectedBuilding].Id,
                typeOfFinishingIndex, premiseNumber, area, floorNumber, availAbilityOfPhoneNumber,
                tempRentalPayment);
        }


        /// <summary>
        /// Удаление помещения
        /// </summary>
        /// <param name="row"></param>
        public void DeletePremises(DataRow row)
        {
            _premiseService.Delete(row);
        }


    }
}

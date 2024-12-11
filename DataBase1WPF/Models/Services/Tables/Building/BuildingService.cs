using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.DataBase.Repositories;
using DataBase1WPF.Models.Services.Tables.Premise;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Building
{
    public class BuildingService : IBuildingService, ITableService
    {
        private IPremiseService _premiseService; 
        
        private Dictionary<DataRow, IBuildingDB> _dataDictionary;
        private List<IBuildingDB> GetValues()
        {
            List<IBuildingDB> values = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();
            return values;
        }

        public BuildingService()
        {
            _premiseService = new PremiseService();
        }


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

            //DataTable tableForView = table.Copy();
            //tableForView.Columns.Remove(tableForView.Columns[1]);
            //tableForView.Columns.Remove(tableForView.Columns[2]);
            //return tableForView;

            
            return table;
        }

        public string GetTableName()
        {
            return "Здания";
        }

        public string GetOtherTableName()
        {
            return "Помещения";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            return new DataTable();
            //List<IBuildingDB> values = GetValues().Where(item => item.Title.Contains(searchLine)).ToList();
            //DataTable table = DataTableConverter.ToDataTable(values);
            //table.Columns.Remove(table.Columns[0]);

            //_dataDictionary = new();
            //for (int i = 0; i < values.Count; i++)
            //    _dataDictionary.Add(table.Rows[i], values[i]);


            //return table;
        }


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


        public List<string> GetDistricts()
        {
            List<string> districts = new();

            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            foreach(IHandbookDB districtDB in districtsDB) 
                districts.Add(districtDB.Title);
            

            return districts;
        }

        public List<string> GetStreets()
        {
            List<string> streets = new();

            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            foreach (IHandbookDB streetDB in streetsDB)
                streets.Add(streetDB.Title);


            return streets;
        }




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


        public int GetDistrictSelectedIndex(DataRow row)
        {
            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            return districtsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].DistrictId);
             
        }

        public int GetStreetsSelectedIndex(DataRow row)
        {
            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            return streetsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].StreetId);

        }


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

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().BuildingDB_Repository.Delete(_dataDictionary[row].Id);
        }


        public DataTable? GetPremisesByBuilding(DataRow row)
        {
            return _premiseService.GetPremisesByBuilding(_dataDictionary[row].Id);
        }

        public DataTable SearchDataInTablePremises(string searchLine)
        {
            return _premiseService.SearchDataInTable(searchLine);
        }


        public void AddPremises(string title)
        {
            _premiseService.Add(title);
        }

        public void UpdatePremises(DataRow row, string title)
        {
            _premiseService.Update(row, title);
        }

        public void DeletePremises(DataRow row)
        {
            _premiseService.Delete(row);
        }


    }
}

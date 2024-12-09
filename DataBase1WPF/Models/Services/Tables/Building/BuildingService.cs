using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
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
        private Dictionary<DataRow, IBuildingDB> _dataDictionary;
        private List<IBuildingDB> GetValues()
        {
            List<IBuildingDB> values = DataManager.GetInstance().BuildingDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IBuildingDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

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


        public void Add(string title)
        {
            DataManager.GetInstance().BankDB_Repository.Create(new HandbookDB(title));
        }

        public void Update(DataRow row, string title)
        {
            DataManager.GetInstance().BankDB_Repository.Update(new HandbookDB(_dataDictionary[row].Id, title));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().BuildingDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

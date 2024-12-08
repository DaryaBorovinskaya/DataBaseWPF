using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class TypesOfFinishingService :   ITableService
    {
        private Dictionary<DataRowWithIndex, IHandbookDB> _dataDictionary;
        private List<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IHandbookDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(new DataRowWithIndex(table.Rows[i], i), values[i]);

            return table;
        }
        public string GetTableName()
        {
            return "Виды отделки";
        }

        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues().Where(item=> item.Title.Contains(searchLine)).ToList());
            table.Columns.Remove(table.Columns[0]);
            return table;
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
        public void Delete(int selectedIndex)
        {
            uint id = 0;
            foreach (KeyValuePair<DataRowWithIndex, IHandbookDB> data in _dataDictionary)
            {
                if (data.Key.Index == selectedIndex)
                    id = data.Value.Id;
            }

            DataManager.GetInstance().TypeOfFinishingDB_Repository.Delete(id);
        }
    }
}

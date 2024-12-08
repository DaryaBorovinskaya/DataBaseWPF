using DataBase1WPF.DataBase.Entities.Fine;
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
    public class FineService :  ITableService
    {
        private Dictionary<DataRowWithIndex, IFineDB> _dataDictionary;
        private List<IFineDB> GetValues()
        {
            List<IFineDB> values = DataManager.GetInstance().FineDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IFineDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(new DataRowWithIndex(table.Rows[i], i), values[i]);

            return table;
        }

        public string GetTableName()
        {
            return "Штраф";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues().Where(item => item.Amount.ToString().Contains(searchLine)).ToList());
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
            foreach (KeyValuePair<DataRowWithIndex, IFineDB> data in _dataDictionary)
            {
                if (data.Key.Index == selectedIndex)
                    id = data.Value.Id;
            }

            DataManager.GetInstance().FineDB_Repository.Delete(id);
        }
    }
}

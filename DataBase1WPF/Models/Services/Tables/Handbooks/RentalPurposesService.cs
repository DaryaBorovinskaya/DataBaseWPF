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
    public class RentalPurposesService : ITableService
    {
        private List<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues());
            table.Columns.Remove(table.Columns[0]);
            return table;
        }
        public string GetTableName()
        {
            return "Цели аренды";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues().Where(item => item.Title.Contains(searchLine)).ToList());
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
    }
}

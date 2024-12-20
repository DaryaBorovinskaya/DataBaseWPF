using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.UserManagement
{
    public class UserManagementService : ITableService
    {
        private Dictionary<DataRow, IUserAbilitiesDB> _dataDictionary;
        private List<IUserAbilitiesDB> GetValues()
        {
            List<IUserAbilitiesDB> values = DataManager.GetInstance().UserAbilitiesDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IUserAbilitiesDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }

        public string GetTableName()
        {
            return "Права пользователей";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IUserAbilitiesDB> values = GetValues().Where(item => 
            item.UserLogin.Contains(searchLine)
            || item.MenuElemName.Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


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


        public List<string> GetUsers()
        {
            List<string> users = new();

            List<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read().ToList();

            foreach (IUserDB userDB in usersDB)
                users.Add(userDB.Login);


            return users;
        }


        public List<IMenuElemDB> GetMenuElemsDB()
        {
            List<IMenuElemDB> menuElems = new();

            List<IMenuElemDB> menuElemsDB = DataManager.GetInstance().MenuElemDB_Repository.Read().ToList();

            foreach (IMenuElemDB menuElemDB in menuElemsDB)
            {
                if (menuElemDB.FuncName != null && menuElemDB.FuncName != string.Empty)
                    menuElems.Add(menuElemDB);
            }

            return menuElems;
        }

        public List<string> GetMenuElems()
        {
            List<string> menuElems = new();

            List<IMenuElemDB> menuElemsDB = DataManager.GetInstance().MenuElemDB_Repository.Read().ToList();

            foreach (IMenuElemDB menuElemDB in menuElemsDB)
            {
                if (menuElemDB.FuncName != null && menuElemDB.FuncName != string.Empty)
                    menuElems.Add(menuElemDB.Name);
            }

            return menuElems;
        }

        public int GetUserSelectedIndex(DataRow row)
        {
            List<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read().ToList();

            return usersDB.FindIndex((elem) => elem.Id == _dataDictionary[row].UserId);

        }

        public int GetMenuElemSelectedIndex(DataRow row)
        {
            return GetMenuElemsDB().FindIndex((elem) => elem.Id == _dataDictionary[row].MenuElemId);

        }
        
        public void Add(int userSelectedIndex, 
            int menuElemSelectedIndex, bool canRead,
            bool canWrite, bool canEdit, bool canDelete)
        {
            DataManager.GetInstance().UserAbilitiesDB_Repository.Create(new UserAbilitiesDB(
                DataManager.GetInstance().UserDB_Repository.Read().ToList()[userSelectedIndex].Id,
                GetMenuElemsDB()[menuElemSelectedIndex].Id,
                canRead,
                canWrite,
                canEdit,
                canDelete
                ));
        }

        public void Update(DataRow row, int userSelectedIndex,
            int menuElemSelectedIndex, bool canRead,
            bool canWrite, bool canEdit, bool canDelete)
        {
            DataManager.GetInstance().UserAbilitiesDB_Repository.Update(new UserAbilitiesDB(
                _dataDictionary[row].Id,
                DataManager.GetInstance().UserDB_Repository.Read().ToList()[userSelectedIndex].Id,
                GetMenuElemsDB()[menuElemSelectedIndex].Id,
                canRead,
                canWrite,
                canEdit,
                canDelete
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().UserAbilitiesDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

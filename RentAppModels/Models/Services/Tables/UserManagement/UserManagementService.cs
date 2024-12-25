using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.UserManagement
{
    /// <summary>
    /// Сервис для управления правами пользователей
    /// </summary>
    public class UserManagementService : ITableService
    {
        private Dictionary<DataRow, IUserAbilitiesDB> _dataDictionary;

        /// <summary>
        /// Получение значений прав пользователей
        /// </summary>
        /// <returns></returns>
        private List<IUserAbilitiesDB> GetValues()
        {
            List<IUserAbilitiesDB> values = DataManager.GetInstance().UserAbilitiesDB_Repository.Read().ToList();
            return values;
        }

        /// <summary>
        /// Получение значений прав пользователей в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IUserAbilitiesDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[4]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }


        /// <summary>
        /// Получение имени таблицы права пользователей
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Права пользователей";
        }

        /// <summary>
        /// Поиск данных по таблице права пользователей
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IUserAbilitiesDB> values = GetValues().Where(item => 
            item.UserLogin.Contains(searchLine)
            || item.MenuElemName.Contains(searchLine)
            || item.Surname.Contains(searchLine)
            || item.Name.Contains(searchLine)
            || item.Patronymic.Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[4]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение индекса сотрудника у выбранного значения права пользователя
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetEmployeesSelectedIndex(DataRow row)
        {
            List<IEmployeeDB> employeesDB = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();

            return employeesDB.FindIndex((elem) => elem.Id == _dataDictionary[row].EmployeeId);
        }


        /// <summary>
        /// Получение прав пользователя к разделу управления правами пользователя
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
        /// Получение пользователей
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsers()
        {
            List<string> users = new();

            List<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read().ToList();

            foreach (IUserDB userDB in usersDB)
                users.Add(userDB.Login + " - " + userDB.Surname + " " + userDB.Name 
                    + " " + userDB.Patronymic);


            return users;
        }

        /// <summary>
        /// Получение элементов меню ( возврат значений из базы данных )
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение списка элементов меню (список строк)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение индекса пользователя у выбранного значения права пользователя
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetUserSelectedIndex(DataRow row)
        {
            List<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read().ToList();

            return usersDB.FindIndex((elem) => elem.Id == _dataDictionary[row].UserId);

        }

        /// <summary>
        /// Получение индекса элемента меню у выбранного значения права пользователя
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetMenuElemSelectedIndex(DataRow row)
        {
            return GetMenuElemsDB().FindIndex((elem) => elem.Id == _dataDictionary[row].MenuElemId);

        }
        

        /// <summary>
        /// Добавление права пользователя
        /// </summary>
        /// <param name="userSelectedIndex"></param>
        /// <param name="menuElemSelectedIndex"></param>
        /// <param name="canRead"></param>
        /// <param name="canWrite"></param>
        /// <param name="canEdit"></param>
        /// <param name="canDelete"></param>
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


        /// <summary>
        /// Изменение права пользователя
        /// </summary>
        /// <param name="row"></param>
        /// <param name="userSelectedIndex"></param>
        /// <param name="menuElemSelectedIndex"></param>
        /// <param name="canRead"></param>
        /// <param name="canWrite"></param>
        /// <param name="canEdit"></param>
        /// <param name="canDelete"></param>
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

        /// <summary>
        /// Удаление права пользователя
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().UserAbilitiesDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

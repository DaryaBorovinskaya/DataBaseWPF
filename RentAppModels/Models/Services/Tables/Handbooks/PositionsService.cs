using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    /// <summary>
    /// Сервис для должностей
    /// </summary>
    public class PositionsService :  ITableService
    {
        private Dictionary<DataRow, IPositionDB> _dataDictionary;

        /// <summary>
        /// Получение данных должностей
        /// </summary>
        /// <returns></returns>
        private List<IPositionDB> GetValues()
        {
            List<IPositionDB> values = DataManager.GetInstance().PositionDB_Repository.Read().ToList();
            return values;
        }


        /// <summary>
        /// Получение данных должностей в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IPositionDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }

        /// <summary>
        /// Получение имени таблицы должности
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Должности";
        }

        /// <summary>
        /// Поиск данных по таблице должности
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IPositionDB> values = GetValues().Where(
                item => (item.Name.Contains(searchLine) || item.Salary.ToString().Contains(searchLine))).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение прав пользователей к должностям
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
        /// Добавление должности
        /// </summary>
        /// <param name="title"></param>
        /// <param name="salary"></param>
        public void Add(string title, float salary)
        {
            DataManager.GetInstance().PositionDB_Repository.Create(new PositionDB(title, salary));
        }


        /// <summary>
        /// Изменение должности
        /// </summary>
        /// <param name="row"></param>
        /// <param name="title"></param>
        /// <param name="salary"></param>
        public void Update(DataRow row, string title, float salary) 
        {
            DataManager.GetInstance().PositionDB_Repository.Update(new PositionDB(_dataDictionary[row].Id, title, salary));
        }

        /// <summary>
        /// Удаление должности
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PositionDB_Repository.Delete(_dataDictionary[row].Id);
        }
    } 
}

using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    /// <summary>
    /// Сервис для районов
    /// </summary>
    public class DistrictsService :   ITableService
    {
        private Dictionary<DataRow, IHandbookDB> _dataDictionary;

        /// <summary>
        /// Получение данных районов
        /// </summary>
        /// <returns></returns>
        private List<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();
            return values;
        }

        /// <summary>
        /// Получение данных районов в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IHandbookDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }

        /// <summary>
        /// Получение имени таблицы районы
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Районы";
        }

        /// <summary>
        /// Поиск данных по таблице районы
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IHandbookDB> values = GetValues().Where(item => item.Title.Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение прав пользователей к районам
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
        /// Добавление района
        /// </summary>
        /// <param name="title"></param>
        public void Add(string title)
        {
            DataManager.GetInstance().DistrictDB_Repository.Create(new HandbookDB(title));
        }

        /// <summary>
        /// Изменение района
        /// </summary>
        /// <param name="row"></param>
        /// <param name="title"></param>
        public void Update(DataRow row, string title)
        {
            DataManager.GetInstance().DistrictDB_Repository.Update(new HandbookDB(_dataDictionary[row].Id, title));
        }

        /// <summary>
        /// Удаление района
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().DistrictDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

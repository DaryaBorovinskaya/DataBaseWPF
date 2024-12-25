using DataBase1WPF.DataBase.Entities.Fine;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    /// <summary>
    /// Сервис для штрафов
    /// </summary>
    public class FineService :  ITableService
    {
        private Dictionary<DataRow, IFineDB> _dataDictionary;

        /// <summary>
        /// Получение данных штрафов
        /// </summary>
        /// <returns></returns>
        private List<IFineDB> GetValues()
        {
            List<IFineDB> values = DataManager.GetInstance().FineDB_Repository.Read().ToList();
            return values;
        }

        /// <summary>
        /// Получение данных штрафов в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IFineDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }


        /// <summary>
        /// Получение имени таблицы штраф
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Штраф";
        }


        /// <summary>
        /// Поиск данных по таблице штрафы
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IFineDB> values = GetValues().Where(item => item.Amount.ToString().Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение прав пользователей к штрафам
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
        /// Добавление штрафа
        /// </summary>
        /// <param name="amount"></param>
        public void Add(float amount)
        {
            DataManager.GetInstance().FineDB_Repository.Create(new FineDB(amount));
        }

        /// <summary>
        /// Изменение штрафа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="amount"></param>
        public void Update(DataRow row, float amount) 
        {
            DataManager.GetInstance().FineDB_Repository.Update(new FineDB(_dataDictionary[row].Id, amount));
        }

        /// <summary>
        /// Удаление штрафа
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().FineDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

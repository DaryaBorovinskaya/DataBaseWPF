﻿using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    /// <summary>
    /// Сервис для улиц
    /// </summary>
    public class StreetsService : ITableService
    {
        private Dictionary<DataRow, IHandbookDB> _dataDictionary;

        /// <summary>
        /// Получение данных улиц
        /// </summary>
        /// <returns></returns>
        private List<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().StreetDB_Repository.Read().ToList();
            return values;
        }

        /// <summary>
        /// Получение данных улиц в таблице DataTable
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
        /// Получение имени таблицы улицы
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Улицы";
        }

        /// <summary>
        /// Поиск данных по таблице улицы 
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
        /// Получение прав пользователей к улицам
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
        /// Добавление улицы
        /// </summary>
        /// <param name="title"></param>
        public void Add(string title)
        {
            DataManager.GetInstance().StreetDB_Repository.Create(new HandbookDB(title));
        }

        /// <summary>
        /// Изменение улицы
        /// </summary>
        /// <param name="row"></param>
        /// <param name="title"></param>
        public void Update(DataRow row, string title)
        {
            DataManager.GetInstance().StreetDB_Repository.Update(new HandbookDB(_dataDictionary[row].Id, title));
        }

        /// <summary>
        /// Удаление улицы
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().StreetDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

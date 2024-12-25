using System.Data;

namespace DataBase1WPF.Models.Services.Tables
{
    /// <summary>
    /// Базовый интерфейс для сервисов данных из базы данных в табличной форме
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// Получение значений в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable();

        /// <summary>
        /// Получение имени таблицы
        /// </summary>
        /// <returns></returns>
        public string GetTableName();

        /// <summary>
        /// Поиск данных по таблице
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine);

        /// <summary>
        /// Получение прав пользователя к текущему разделу
        /// </summary>
        /// <param name="menuElemId"></param>
        /// <returns></returns>
        public UserAbilitiesType GetUserAbilities(uint menuElemId);

        /// <summary>
        /// Удаление строки из таблицы
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row);

    }
}

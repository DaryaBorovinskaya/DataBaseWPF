namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Базовый интерфейс для взаимодействия с базой данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryDB<T> where T : class
    {
        /// <summary>
        /// Добаление в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(T entity);

        /// <summary>
        /// Чтение из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<T> Read();

        /// <summary>
        /// Изменение данных из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);

        /// <summary>
        /// Удаление из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id);
        
    }
}

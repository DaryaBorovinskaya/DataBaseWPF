namespace DataBase1WPF.DataBase.Repositories
{
    public interface IRepositoryDB<T> where T : class
    {
        public void Create(T entity);
        public IList<T> Read();
        public void Update(T entity);
        public void Delete(uint id);
        
    }
}

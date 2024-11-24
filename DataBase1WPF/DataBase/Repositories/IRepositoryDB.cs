using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public interface IRepositoryDB<T> where T : class
    {
        public void Create(T entity);
        public IList<T> Read();
        public void Update(T entity);
        public void Delete(T entity);
        
    }
}

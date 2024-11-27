using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class UserDB_Repository : IRepositoryDB <IUserDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IUserDB entity)
        {
            _query = $"insert into users " +
                     $"(login, password) " +
                     $"values ('{entity.Login}', '{entity.Password}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IUserDB> Read()
        {
            _query = "select * from users";
            IList<IUserDB> result = new List<IUserDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new UserDB(
                    uint.Parse(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString()
                ));
            }
            return result;
        }

        public void Update(IUserDB entity)
        {
            _query = $"update users set " +
                     $"login='{entity.Login}', password='{entity.Password}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IUserDB entity)
        {
            _query = $"delete from users where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

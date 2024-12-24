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
                     $"(login, password, employee_id) " +
                     $"values ('{entity.Login}', '{entity.Password}', {entity.EmployeeId})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IUserDB> Read()
        {
            _query = "SELECT rentapp.users.id ,\r\nrentapp.users.login, " +
                " rentapp.users.password, " +
                " rentapp.users.employee_id,\r\nrentapp.employees.surname, " +
                " rentapp.employees.name,\r\nrentapp.employees.patronymic " +
                " FROM rentapp.users " +
                " join rentapp.employees " +
                " on rentapp.users.employee_id = rentapp.employees.id " +
                " order by rentapp.users.id;";
            IList<IUserDB> result = new List<IUserDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new UserDB(
                    uint.Parse(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString(),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString()
                ));
            }
            return result;
        }

        public void Update(IUserDB entity)
        {
            _query = $"update users set " +
                     $"login='{entity.Login}', password='{entity.Password}', employee_id={entity.EmployeeId} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from users where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

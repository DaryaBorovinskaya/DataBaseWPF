using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class UserAbilitiesDB_Repository : IRepositoryDB<IUserAbilitiesDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IUserAbilitiesDB entity)
        {
            _query = $"insert into users_abilities " +
                     $"(user_id, menu_element_id, r, w, e, d) " +
                     $"values ({entity.UserId}, {entity.MenuElemId}, {entity.R}," +
                     $"{entity.W}, {entity.E}, {entity.D})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IUserAbilitiesDB> Read()
        {
            _query = "SELECT rentapp.users_abilities.id, " +
                "rentapp.users_abilities.user_id, " +
                "rentapp.users.login as user_login, " +
                "rentapp.users_abilities.menu_element_id, " +
                "rentapp.menu_elements.name as menu_element_name, " +
                "rentapp.users_abilities.r, " +
                "rentapp.users_abilities.w, " +
                "rentapp.users_abilities.e, " +
                "rentapp.users_abilities.d " +
                "FROM rentapp.users_abilities " +
                "left outer join rentapp.users " +
                " on rentapp.users_abilities.user_id = rentapp.users.id " +
                "left outer join rentapp.menu_elements on " +
                " rentapp.users_abilities.menu_element_id = rentapp.menu_elements.id " +
                "order by rentapp.users_abilities.id;";
            IList<IUserAbilitiesDB> result = new List<IUserAbilitiesDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new UserAbilitiesDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    int.Parse(row[5].ToString()) == 1 ? true : false,
                    int.Parse(row[6].ToString()) == 1 ? true : false,
                    int.Parse(row[7].ToString()) == 1 ? true : false,
                    int.Parse(row[8].ToString()) == 1 ? true : false
                ));
            }
            return result;
        }

        public void Update(IUserAbilitiesDB entity)
        {
            _query = $"update users_abilities set " +
                     $"user_id={entity.UserId}, menu_element_id={entity.MenuElemId}, " +
                     $"r={entity.R}, w={entity.W}, e={entity.E}, d={entity.D} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from users_abilities where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

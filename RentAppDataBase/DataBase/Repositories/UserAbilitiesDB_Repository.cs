using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с правами пользователей из базы данных
    /// </summary>
    public class UserAbilitiesDB_Repository : IRepositoryDB<IUserAbilitiesDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового права пользователя в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IUserAbilitiesDB entity)
        {
            _query = $"insert into users_abilities " +
                     $"(user_id,  menu_element_id, r, w, e, d) " +
                     $"values ({entity.UserId},  {entity.MenuElemId}, {entity.R}," +
                     $"{entity.W}, {entity.E}, {entity.D})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о правах пользователей из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IUserAbilitiesDB> Read()
        {
            _query = "SELECT rentapp.users_abilities.id, " +
                "rentapp.users_abilities.user_id, " +
                "rentapp.users.login as user_login, " +
                "rentapp.users.employee_id as employee_id, " +
                "rentapp.employees.surname as surname, " +
                "rentapp.employees.name as name, " +
                "rentapp.employees.patronymic as patronymic, " +
                "rentapp.users_abilities.menu_element_id, "+
                "rentapp.menu_elements.name as menu_element_name, "+
                "rentapp.users_abilities.r, "+
                "rentapp.users_abilities.w, "+
                "rentapp.users_abilities.e, "+
                "rentapp.users_abilities.d "+
                "FROM rentapp.users_abilities "+
                "left outer join rentapp.users "+
                " on rentapp.users_abilities.user_id = rentapp.users.id "+
                "left outer join rentapp.menu_elements on "+
                "rentapp.users_abilities.menu_element_id = rentapp.menu_elements.id "+
                "left outer join rentapp.employees on "+
                "rentapp.users.employee_id = rentapp.employees.id "+
                "order by rentapp.users_abilities.id; ";
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
                    row[5].ToString(),
                    row[6].ToString(),
                    uint.Parse(row[7].ToString()),
                    row[8].ToString(),
                    int.Parse(row[9].ToString()) == 1 ? true : false,
                    int.Parse(row[10].ToString()) == 1 ? true : false,
                    int.Parse(row[11].ToString()) == 1 ? true : false,
                    int.Parse(row[12].ToString()) == 1 ? true : false
                ));
            }
            return result;
        }

        /// <summary>
        /// Изменение данных права пользователя из базы данных
        /// </summary>
        /// <param name="entity"></param>

        public void Update(IUserAbilitiesDB entity)
        {
            _query = $"update users_abilities set " +
                     $"user_id={entity.UserId},  " +
                     $" menu_element_id={entity.MenuElemId}, " +
                     $"r={entity.R}, w={entity.W}, e={entity.E}, d={entity.D} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление права пользователя из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from users_abilities where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

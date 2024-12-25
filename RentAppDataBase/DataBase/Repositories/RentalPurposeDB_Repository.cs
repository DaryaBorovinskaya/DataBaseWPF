using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с целями аренды из базы данных
    /// </summary>
    public class RentalPurposeDB_Repository : IRepositoryDB<IHandbookDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление новой цели аренды в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IHandbookDB entity)
        {
            _query = $"insert into rental_purposes " +
                     $"(title) " +
                     $"values ('{entity.Title}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о целях аренды из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IHandbookDB> Read()
        {
            _query = "select * from rental_purposes";
            IList<IHandbookDB> result = new List<IHandbookDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new HandbookDB(
                    uint.Parse(row[0].ToString()),
                    row[1].ToString()
                ));
            }
            return result;
        }

        /// <summary>
        /// Изменение данных о цели аренды из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IHandbookDB entity)
        {
            _query = $"update rental_purposes set " +
                     $"title='{entity.Title}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление цели аренды из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from rental_purposes where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

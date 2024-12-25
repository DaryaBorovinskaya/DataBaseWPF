using DataBase1WPF.DataBase.Entities.Fine;
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
    /// Взаимодействие со штрафами из базы данных
    /// </summary>
    public class FineDB_Repository : IRepositoryDB<IFineDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового штрафа в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IFineDB entity)
        {
            _query = $"insert into fine " +
                     $"(amount) " +
                     $"values ({entity.Amount})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных штрафов из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IFineDB> Read()
        {
            _query = "select * from fine";
            IList<IFineDB> result = new List<IFineDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new FineDB(
                    uint.Parse(row[0].ToString()),
                    float.Parse(row[1].ToString())
                ));
            }
            return result;
        }

        /// <summary>
        /// Изменение данных штрафа из базы даныых
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IFineDB entity)
        {
            _query = $"update fine set " +
                     $"amount={entity.Amount} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление штрафа из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from fine where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

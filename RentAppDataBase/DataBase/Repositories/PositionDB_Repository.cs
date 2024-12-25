using DataBase1WPF.DataBase.Entities.Position;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с должностями из базы данных
    /// </summary>
    public class PositionDB_Repository : IRepositoryDB<IPositionDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление новой должности в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IPositionDB entity)
        {
            _query = $"insert into positions (name, salary) values ('{entity.Name}', {entity.Salary})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о должностях из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IPositionDB> Read()
        {
            _query = "select * from positions";
            IList<IPositionDB> result = new List<IPositionDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PositionDB(uint.Parse(row[0].ToString()), row[1].ToString(), float.Parse(row[2].ToString())));
            }
            return result;
        }


        /// <summary>
        /// Изменение данных должности из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IPositionDB entity)
        {
            _query = $"update positions set name='{entity.Name}', salary={entity.Salary} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаение должности из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from positions where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        
    }
}

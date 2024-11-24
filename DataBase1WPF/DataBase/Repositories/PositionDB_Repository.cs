using DataBase1WPF.DataBase.Entities.Position;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    class PositionDB_Repository : IRepositoryDB<IPositionDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IPositionDB entity)
        {
            _query = $"insert into positions (name, salary) values ('{entity.Name}', {entity.Salary})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
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

        public void Update(IPositionDB entity)
        {
            _query = $"update positions set name='{entity.Name}', salary={entity.Salary} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IPositionDB entity)
        {
            _query = $"delete from positions where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        
    }
}

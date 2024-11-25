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
    public class FineDB_Repository : IRepositoryDB<IFineDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IFineDB entity)
        {
            _query = $"insert into fine " +
                     $"(amount)" +
                     $"values ({entity.Amount})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
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

        public void Update(IFineDB entity)
        {
            _query = $"update fine set " +
                     $"amount={entity.Amount}" +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IFineDB entity)
        {
            _query = $"delete from fine where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

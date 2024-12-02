using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class OrderDB_Repository : IRepositoryDB<IOrderDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IOrderDB entity)
        {
            _query = $"insert into orders " +
                     $"(premise_id, rental_purpose_id, contract_id, begin_of_rent, " +
                     $"end_of_rent, rental_payment) " +
                     $"values ({entity.PremiseID}, {entity.RentalPurposeId}," +
                     $"{entity.ContractId}, '{entity.BeginOfRent.ToString("yyyy-MM-dd")}', '{entity.EndOfRent.ToString("yyyy-MM-dd")}'," +
                     $"{entity.RentalPayment})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IOrderDB> Read()
        {
            _query = "select * from orders";
            IList<IOrderDB> result = new List<IOrderDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new OrderDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    DateTime.Parse(row[4].ToString()),
                    DateTime.Parse(row[5].ToString()),
                    float.Parse(row[6].ToString())
                ));
            }
            return result;
        }

        public void Update(IOrderDB entity)
        {
            _query = $"update orders set " +
                     $"premise_id={entity.PremiseID}, rental_purpose_id={entity.RentalPurposeId}, " +
                     $"contract_id={entity.ContractId}, begin_of_rent='{entity.BeginOfRent.ToString("yyyy-MM-dd")}', " +
                     $"end_of_rent='{entity.EndOfRent.ToString("yyyy-MM-dd")}', rental_payment={entity.RentalPayment} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IOrderDB entity)
        {
            _query = $"delete from orders where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

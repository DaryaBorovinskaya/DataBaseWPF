using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class PaymentDB_Repository : IRepositoryDB<IPaymentDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IPaymentDB entity)
        {
            _query = $"insert into payments " +
                     $"(individual_id, juridical_person_id, contract_id, date_of_payment, " +
                     $"amount_of_payment) " +
                     $"values ({entity.IndividualId}, {entity.JuridicalPersonId}," +
                     $"{entity.ContractId}, '{entity.DateOfPayment.ToString("yyyy-MM-dd")}', {entity.AmountOfPayment})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IPaymentDB> Read()
        {
            _query = "select * from payments";
            IList<IPaymentDB> result = new List<IPaymentDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PaymentDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    DateOnly.Parse(row[4].ToString()),
                    float.Parse(row[6].ToString())
                ));
            }
            return result;
        }

        public void Update(IPaymentDB entity)
        {
            _query = $"update payments set " +
                     $"individual_id={entity.IndividualId}, juridical_person_id={entity.JuridicalPersonId}, " +
                     $"contract_id={entity.ContractId}, date_of_payment='{entity.DateOfPayment.ToString("yyyy-MM-dd")}', " +
                     $"amount_of_payment={entity.AmountOfPayment} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IPaymentDB entity)
        {
            _query = $"delete from payments where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

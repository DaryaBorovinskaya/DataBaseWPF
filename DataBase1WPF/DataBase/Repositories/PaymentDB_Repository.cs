using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Payment;
using DataBase1WPF.DataBase.Entities.Premise;
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
                     $"(contract_id, date_of_payment, " +
                     $"amount_of_payment) " +
                     $"values ({entity.ContractId}, '{entity.DateOfPayment}', {entity.AmountOfPayment})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IPaymentDB> Read()
        {
            throw new NotImplementedException();
            
        }

        public void Update(IPaymentDB entity)
        {
            _query = $"update payments set " +
                     $"contract_id={entity.ContractId}, date_of_payment='{entity.DateOfPayment}', " +
                     $"amount_of_payment={entity.AmountOfPayment} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from payments where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        public IList<IPaymentDB> GetPaymentsByContractId(uint contract_id)
        {
            _query = $"SELECT  rentapp.payments.id,  COALESCE(rentapp.payments.contract_id, 0) AS contract_id,  rentapp.payments.date_of_payment,\r\n    rentapp.payments.amount_of_payment\r\nFROM \r\n    rentapp.payments \r\nLEFT OUTER JOIN \r\n    rentapp.contracts ON rentapp.payments.contract_id = rentapp.contracts.id\r\nwhere rentapp.payments.contract_id = {contract_id}\r\nGROUP BY \r\n    rentapp.payments.id,\r\n    rentapp.payments.date_of_payment,\r\n    rentapp.payments.amount_of_payment\r\nORDER BY \r\n    rentapp.payments.id;\r\n\r\n";
            IList<IPaymentDB> result = new List<IPaymentDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PaymentDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[3].ToString().Substring(0, 10),
                    float.Parse(row[4].ToString())
                ));
            }
            return result;
        }
    }
}

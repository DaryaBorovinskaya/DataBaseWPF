using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class ContractDB_Repository :IRepositoryDB<IContractDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IContractDB entity)
        {
            _query = $"insert into contracts " +
                     $"(individual_id, juridical_person_id," +
                     $"employee_id, payment_frequency_id, registration_number," +
                     $"begin_of_action, end_of_action, additional_conditions, fine) " +
                     $"values ({entity.IndividualId}, {entity.JuridicalPersonId}," +
                     $"{entity.EmployeeId}, {entity.PaymentFrequencyId}," +
                     $"'{entity.RegistrationNumber}', '{entity.BeginOfAction.ToString("yyyy-MM-dd")}', " +
                     $"'{entity.EndOfAction.ToString("yyyy-MM-dd")}', '{entity.AdditionalConditions}'," +
                     $"{entity.Fine})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IContractDB> Read()
        {
            _query = "select * from contracts";
            IList<IContractDB> result = new List<IContractDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new ContractDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    uint.Parse(row[4].ToString()),
                    row[5].ToString(),
                    DateTime.Parse(row[6].ToString()),
                    DateTime.Parse(row[7].ToString()),
                    row[8].ToString(),
                    float.Parse(row[9].ToString())
                ));
            }
            return result;
        }

        public void Update(IContractDB entity)
        {
            _query = $"update contracts set " +
                     $"individual_id={entity.IndividualId}, juridical_person_id={entity.JuridicalPersonId}," +
                     $"employee_id={entity.EmployeeId}, payment_frequency_id={entity.PaymentFrequencyId}," +
                     $"registration_number='{entity.RegistrationNumber}', begin_of_action='{entity.BeginOfAction.ToString("yyyy-MM-dd")}'," +
                     $"end_of_action='{entity.EndOfAction.ToString("yyyy-MM-dd")}', additional_conditions='{entity.AdditionalConditions}'," +
                     $"fine={entity.Fine} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IContractDB entity)
        {
            _query = $"delete from contracts where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

using DataBase1WPF.DataBase.Entities.Individual;
using System.Data;

namespace DataBase1WPF.DataBase.Repositories
{
    class IndividualDB_Repository : IRepositoryDB<IIndividualDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IIndividualDB entity)
        {
            _query = $"insert into individuals " +
                     $"(surname, name, patronymic, phone_number, passport_series, " +
                     $"passport_number, date_of_issue, issued_by) " +
                     $"values ('{entity.Surname}', '{entity.Name}'," +
                     $"'{entity.Patronymic}', '{entity.PhoneNumber}'," +
                     $"'{entity.PassportSeries}', '{entity.PassportNumber}'," +
                     $"'{entity.DateOfIssue.ToString("yyyy-MM-dd")}', '{entity.IssuedBy}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IIndividualDB> Read()
        {
            _query = "select * from individuals";
            IList<IIndividualDB> result = new List<IIndividualDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new IndividualDB(
                    uint.Parse(row[0].ToString()), 
                    row[1].ToString(), 
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString(),
                    DateTime.Parse(row[7].ToString()),
                    row[8].ToString()    
                ));
            }
            return result;
        }

        public void Update(IIndividualDB entity)
        {
            _query = $"update individuals set " +
                     $"surname='{entity.Surname}', name='{entity.Name}', " +
                     $"patronymic='{entity.Patronymic}', phone_number='{entity.PhoneNumber}', " +
                     $"passport_series='{entity.PassportSeries}', passport_number='{entity.PassportNumber}', " +
                     $"date_of_issue='{entity.DateOfIssue.ToString("yyyy-MM-dd")}', issued_by='{entity.IssuedBy}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IIndividualDB entity)
        {
            _query = $"delete from individuals where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

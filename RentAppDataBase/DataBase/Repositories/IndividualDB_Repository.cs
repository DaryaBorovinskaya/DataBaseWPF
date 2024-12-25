using DataBase1WPF.DataBase.Entities.Individual;
using System.Data;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с физическими лицами из базы данных
    /// </summary>
    public class IndividualDB_Repository : IRepositoryDB<IIndividualDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового физического лица в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IIndividualDB entity)
        {
            _query = $"insert into individuals " +
                     $"(surname, name, patronymic, phone_number, passport_series, " +
                     $"passport_number, date_of_issue, issued_by) " +
                     $"values ('{entity.Surname}', '{entity.Name}'," +
                     $"'{entity.Patronymic}', '{entity.PhoneNumber}'," +
                     $"'{entity.PassportSeries}', '{entity.PassportNumber}'," +
                     $"'{entity.DateOfIssue}', '{entity.IssuedBy}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о физических лицах из базы данных
        /// </summary>
        /// <returns></returns>
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
                    row[7].ToString().Substring(0,10),
                    row[8].ToString()    
                ));
            }
            return result;
        }


        /// <summary>
        /// Изменение данных физического лица из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IIndividualDB entity)
        {
            _query = $"update individuals set " +
                     $"surname='{entity.Surname}', name='{entity.Name}', " +
                     $"patronymic='{entity.Patronymic}', phone_number='{entity.PhoneNumber}', " +
                     $"passport_series='{entity.PassportSeries}', passport_number='{entity.PassportNumber}', " +
                     $"date_of_issue='{entity.DateOfIssue}', issued_by='{entity.IssuedBy}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление физического лица из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from individuals where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

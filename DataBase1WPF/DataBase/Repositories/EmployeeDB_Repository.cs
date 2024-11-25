using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Fine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class EmployeeDB_Repository : IRepositoryDB<IEmployeeDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IEmployeeDB entity)
        {
            _query = $"insert into employees " +
                     $"(district_id, street_id, surname, name, patronymic, " +
                     $"date_of_birth, house_number) " +
                     $"values ({entity.DistrictId}, {entity.StreetId}," +
                     $" '{entity.Surname}', '{entity.Name}'," +
                     $"'{entity.Patronymic}', {entity.DateOfBirth}, " +
                     $"'{entity.HouseNumber}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IEmployeeDB> Read()
        {
            _query = "select * from employees";
            IList<IEmployeeDB> result = new List<IEmployeeDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new EmployeeDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    row[4].ToString(),
                    row[5].ToString(),
                    DateOnly.Parse(row[6].ToString()),
                    row[7].ToString()
                ));
            }
            return result;
        }

        public void Update(IEmployeeDB entity)
        {
            _query = $"update employees set " +
                     $"district_id={entity.DistrictId}, street_id={entity.StreetId}, " +
                     $"surname='{entity.Surname}', name='{entity.Name}', " +
                     $"patronymic='{entity.Patronymic}', date_of_birth={entity.DateOfBirth}, " +
                     $"house_number='{entity.HouseNumber}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IEmployeeDB entity)
        {
            _query = $"delete from employees where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

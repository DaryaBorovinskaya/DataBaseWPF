﻿using DataBase1WPF.DataBase.Entities.Employee;
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
                     $"'{entity.Patronymic}', '{entity.DateOfBirth /*entity.DateOfBirth.ToString("yyyy-MM-dd")*/}', " +
                     $"'{entity.HouseNumber}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IEmployeeDB> Read()
        {
            _query = "SELECT  rentapp.employees.id, COALESCE(rentapp.employees.district_id, 0) AS district_id, " +
                "  MAX(rentapp.districts.title) AS district_title,     COALESCE(rentapp.employees.street_id, 0) AS street_id," +
                "    MAX(rentapp.streets.title) AS street_title,    rentapp.employees.surname,  rentapp.employees.name,  " +
                " rentapp.employees.patronymic,    rentapp.employees.date_of_birth,   rentapp.employees.house_number FROM  " +
                "  rentapp.employees LEFT OUTER JOIN    rentapp.districts ON rentapp.employees.district_id = rentapp.districts.id " +
                " LEFT OUTER JOIN     rentapp.streets ON rentapp.employees.street_id = rentapp.streets.id  GROUP BY " +
                "   rentapp.employees.id,    rentapp.employees.surname,    rentapp.employees.name,     rentapp.employees.patronymic," +
                "    rentapp.employees.date_of_birth,   rentapp.employees.house_number ORDER BY    rentapp.employees.id;";
            IList<IEmployeeDB> result = new List<IEmployeeDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new EmployeeDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString(),
                    row[7].ToString(),
                    row[8].ToString().Substring(0,10),
                    row[9].ToString()
                ));
            }
            return result;
        }

        public void Update(IEmployeeDB entity)
        {
            _query = $"update employees set " +
                     $"district_id={entity.DistrictId}, street_id={entity.StreetId}, " +
                     $"surname='{entity.Surname}', name='{entity.Name}', " +
                     $"patronymic='{entity.Patronymic}', date_of_birth='{entity.DateOfBirth /*entity.DateOfBirth.ToString("yyyy-MM-dd")*/}', " +
                     $"house_number='{entity.HouseNumber}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from employees where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

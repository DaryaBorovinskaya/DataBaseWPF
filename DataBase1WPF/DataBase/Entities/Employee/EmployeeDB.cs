using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Employee
{
    public class EmployeeDB : IEmployeeDB
    {
        public uint Id { get; set; }
        public uint DistrictId { get; set; }
        public uint StreetId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string HouseNumber { get; set; }

        public EmployeeDB(uint id, uint districtId, uint streetId, string surname, string name, 
            string? patronymic, DateOnly dateOfBirth, string houseNumber)
        {
            Id = id;
            DistrictId = districtId;
            StreetId = streetId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            HouseNumber = houseNumber;
        }

        public EmployeeDB(uint districtId, uint streetId, string surname, string name, 
            string? patronymic, DateOnly dateOfBirth, string houseNumber)
        {
            DistrictId = districtId;
            StreetId = streetId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            HouseNumber = houseNumber;
        }
    }
}

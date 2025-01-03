﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Employee
{
    /// <summary>
    /// Данные о сотрудниках из базы данных
    /// </summary>
    public class EmployeeDB : IEmployeeDB
    {
        public uint Id { get; set; }
        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }

        public string DateOfBirth { get; set; }
        public uint DistrictId { get; set; }
        public string DistrictTitle { get; set; }
        public uint StreetId { get; set; }
        public string StreetTitle { get; set; }

        

        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }

        public EmployeeDB(uint id, uint districtId, string districtTitle, 
            uint streetId, string streetTitle, string surname, string name, 
            string? patronymic, string dateOfBirth , string houseNumber,
            string flatNumber)
        {
            Id = id;
            DistrictId = districtId;
            DistrictTitle = districtTitle;
            StreetId = streetId;
            StreetTitle = streetTitle;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            HouseNumber = houseNumber;
            FlatNumber = flatNumber;
        }

        public EmployeeDB(uint id, uint districtId, uint streetId, 
            string surname, string name,
            string? patronymic, string dateOfBirth , 
            string houseNumber, string flatNumber)
        {
            Id= id;
            DistrictId = districtId;
            StreetId = streetId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            HouseNumber = houseNumber;
            FlatNumber = flatNumber;
        }


        public EmployeeDB(uint districtId, uint streetId, string surname, string name, 
            string? patronymic, string dateOfBirth , 
            string houseNumber, string flatNumber)
        {
            DistrictId = districtId;
            StreetId = streetId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            HouseNumber = houseNumber;
            FlatNumber = flatNumber;
        }
    }
}

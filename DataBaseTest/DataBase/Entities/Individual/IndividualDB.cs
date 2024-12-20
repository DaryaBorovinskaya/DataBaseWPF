using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Individual
{
    class IndividualDB : IIndividualDB
    {
        public uint Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string /*DateTime*/ DateOfIssue { get; set; }
        public string IssuedBy { get; set; }

        public IndividualDB(uint id, string surname, string name, 
            string? patronymic, string phoneNumber, 
            string passportSeries, string passportNumber, 
            string /*DateTime*/ dateOfIssue, string issuedBy)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            DateOfIssue = dateOfIssue;
            IssuedBy = issuedBy;
        }

        public IndividualDB(string surname, string name, string? patronymic, 
            string phoneNumber, string passportSeries, 
            string passportNumber, string /*DateTime*/ dateOfIssue, string issuedBy)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassportSeries = passportSeries;
            PassportNumber = passportNumber;
            DateOfIssue = dateOfIssue;
            IssuedBy = issuedBy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Individual
{
    public interface IIndividualDB
    {
        public uint Id { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        public string? Patronymic {  get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

        [DisplayName("Серия паспорта")]
        public string PassportSeries { get; set;}

        [DisplayName("Номер паспорта")]
        public string PassportNumber { get; set; }

        [DisplayName("Когда выдан")]
        public string  DateOfIssue { get; set; }

        [DisplayName("Кем выдан")]
        public string IssuedBy { get; set; }
    }
}

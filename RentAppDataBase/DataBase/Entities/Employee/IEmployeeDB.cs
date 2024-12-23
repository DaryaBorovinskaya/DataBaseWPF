using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Employee
{
    public interface IEmployeeDB
    {
        public uint Id { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        public string? Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        public string DateOfBirth { get; set; } 


        [DisplayName("Район")]
        public string DistrictTitle { get; set; }
        public uint DistrictId { get; set; }

        [DisplayName("Улица")]
        public string StreetTitle { get; set; }
        public uint StreetId { get; set; }

        

        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }
        [DisplayName("Номер квартиры")]
        public string FlatNumber { get; set; }
    }
}

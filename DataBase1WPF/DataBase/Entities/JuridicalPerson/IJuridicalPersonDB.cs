using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.JuridicalPerson
{
    public interface IJuridicalPersonDB
    {
        public uint Id { get; set; }
        public uint OrganizationDistrictId { get; set; }
        public uint OrganizationStreetId { get; set; }
        public uint BankId { get; set; }
        

        [DisplayName("Название")]
        public string NameOfOrganization { get; set; }

        [DisplayName("Фамилия руков.")]
        public string DirectorSurname { get; set; }

        [DisplayName("Имя руков.")]
        public string DirectorName { get; set; }

        [DisplayName("Отчество руков.")]
        public string DirectorPatronymic { get; set; }

        [DisplayName("Номер здания")]
        public string OrganizationHouseNumber { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DisplayName("Расчетный счет")]
        public string PaymentAccount { get; set; }

        [DisplayName("ИНН")]
        public string IndividualTaxpayerNumber { get; set; }
    }
}

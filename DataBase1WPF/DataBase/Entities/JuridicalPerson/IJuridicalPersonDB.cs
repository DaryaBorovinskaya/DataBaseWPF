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
        

        [DisplayName("Название")]
        public string NameOfOrganization { get; set; }

        [DisplayName("Фамилия руководителя")]
        public string DirectorSurname { get; set; }

        [DisplayName("Имя руководителя")]
        public string DirectorName { get; set; }

        [DisplayName("Отчество руководителя")]
        public string DirectorPatronymic { get; set; }

        public uint OrganizationDistrictId { get; set; }

        [DisplayName("Район")]
        public string OrganizationDistrictTitle { get; set; }
        public uint OrganizationStreetId { get; set; }

        [DisplayName("Улица")]
        public string OrganizationStreetTitle { get; set; }

        [DisplayName("Номер здания")]
        public string OrganizationHouseNumber { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DisplayName("Расчетный счет")]
        public string PaymentAccount { get; set; }

        public uint BankId { get; set; }

        [DisplayName("Банк")]
        public string BankTitle { get; set; }

        [DisplayName("ИНН")]
        public string IndividualTaxpayerNumber { get; set; }
    }
}

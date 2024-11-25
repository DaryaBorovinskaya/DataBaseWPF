using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.JuridicalPerson
{
    public class JuridicalPersonDB : IJuridicalPersonDB
    {
        public uint Id { get; set; }
        public uint OrganizationDistrictId { get; set; }
        public uint OrganizationStreetId { get; set; }
        public uint BankId { get; set; }

        public string NameOfOrganization { get; set; }

        public string DirectorSurname { get; set; }

        public string DirectorName { get; set; }

        public string DirectorPatronymic { get; set; }

        public string OrganizationHouseNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string PaymentAccount { get; set; }

        public string IndividualTaxpayerNumber { get; set; }

        public JuridicalPersonDB(uint id, uint organizationDistrictId, 
            uint organizationStreetId, uint bankId, string nameOfOrganization, 
            string directorSurname, string directorName, string directorPatronymic, 
            string organizationHouseNumber, string phoneNumber, string paymentAccount, 
            string individualTaxpayerNumber)
        {
            Id = id;
            OrganizationDistrictId = organizationDistrictId;
            OrganizationStreetId = organizationStreetId;
            BankId = bankId;
            NameOfOrganization = nameOfOrganization;
            DirectorSurname = directorSurname;
            DirectorName = directorName;
            DirectorPatronymic = directorPatronymic;
            OrganizationHouseNumber = organizationHouseNumber;
            PhoneNumber = phoneNumber;
            PaymentAccount = paymentAccount;
            IndividualTaxpayerNumber = individualTaxpayerNumber;
        }

        public JuridicalPersonDB( uint organizationDistrictId,
            uint organizationStreetId, uint bankId, string nameOfOrganization,
            string directorSurname, string directorName, string directorPatronymic,
            string organizationHouseNumber, string phoneNumber, string paymentAccount,
            string individualTaxpayerNumber)
        {
            OrganizationDistrictId = organizationDistrictId;
            OrganizationStreetId = organizationStreetId;
            BankId = bankId;
            NameOfOrganization = nameOfOrganization;
            DirectorSurname = directorSurname;
            DirectorName = directorName;
            DirectorPatronymic = directorPatronymic;
            OrganizationHouseNumber = organizationHouseNumber;
            PhoneNumber = phoneNumber;
            PaymentAccount = paymentAccount;
            IndividualTaxpayerNumber = individualTaxpayerNumber;
        }
    }
}

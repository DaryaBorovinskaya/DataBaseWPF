using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class JuridicalPersonDB_Repository : IRepositoryDB<IJuridicalPersonDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IJuridicalPersonDB entity)
        {
            _query = $"insert into juridical_persons " +
                     $"(organization_district_id, organization_street_id, bank_id," +
                     $"name_of_organization, director_surname, " +
                     $"director_name, director_patronymic, " +
                     $"organization_house_number, phone_number, payment_account," +
                     $"individual_taxpayer_number) " +
                     $"values ({entity.OrganizationDistrictId}, {entity.OrganizationStreetId}," +
                     $"{entity.BankId}, '{entity.NameOfOrganization}', '{entity.DirectorSurname}'," +
                     $"'{entity.DirectorName}', '{entity.DirectorPatronymic}'," +
                     $"'{entity.OrganizationHouseNumber}', '{entity.PhoneNumber}', '{entity.PaymentAccount}'," +
                     $"'{entity.IndividualTaxpayerNumber}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IJuridicalPersonDB> Read()
        {
            _query = "select * from juridical_persons";
            IList<IJuridicalPersonDB> result = new List<IJuridicalPersonDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new JuridicalPersonDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    row[5].ToString(),
                    row[6].ToString(),
                    row[7].ToString(),
                    row[8].ToString(),
                    row[9].ToString(),
                    row[10].ToString(),
                    row[11].ToString()
                ));
            }
            return result;
        }

        public void Update(IJuridicalPersonDB entity)
        {
            _query = $"update juridical_persons set " +
                     $"organization_district_id={entity.OrganizationDistrictId}," +
                     $"organization_street_id={entity.OrganizationStreetId}," +
                     $"bank_id={entity.BankId}, name_of_organization='{entity.NameOfOrganization}'," +
                     $"director_surname='{entity.DirectorSurname}'," +
                     $"director_name='{entity.DirectorName}', director_patronymic='{entity.DirectorPatronymic}'," +
                     $"organization_house_number='{entity.OrganizationHouseNumber}'," +
                     $"phone_number='{entity.PhoneNumber}', payment_account='{entity.PaymentAccount}'," +
                     $"individual_taxpayer_number='{entity.IndividualTaxpayerNumber}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IJuridicalPersonDB entity)
        {
            _query = $"delete from juridical_persons where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

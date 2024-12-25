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
    /// <summary>
    /// Взаимодействие с юридическими лицами из базы данных
    /// </summary>
    public class JuridicalPersonDB_Repository : IRepositoryDB<IJuridicalPersonDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового юридического лица в базу данных
        /// </summary>
        /// <param name="entity"></param>
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

        /// <summary>
        /// Чтение данных о юридических лицах из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IJuridicalPersonDB> Read()
        {
            _query = "SELECT   rentapp.juridical_persons.id,  " +
                "  COALESCE(rentapp.juridical_persons.organization_district_id, 0) " +
                " AS organization_district_id, " +
                "   MAX(rentapp.districts.title) AS district_title,  " +
                "  COALESCE(rentapp.juridical_persons.organization_street_id, 0) " +
                " AS organization_street_id, " +
                "    MAX(rentapp.streets.title) AS street_title, " +
                "    COALESCE(rentapp.juridical_persons.bank_id, 0) AS bank_id,  " +
                "  MAX(rentapp.banks.title) AS bank_title, " +
                "  rentapp.juridical_persons.name_of_organization, " +
                "   rentapp.juridical_persons.director_surname,  " +
                "  rentapp.juridical_persons.director_name, " +
                "  rentapp.juridical_persons.director_patronymic, " +
                "    rentapp.juridical_persons.organization_house_number, " +
                "   rentapp.juridical_persons.phone_number,  " +
                " rentapp.juridical_persons.payment_account, " +
                "   rentapp.juridical_persons.individual_taxpayer_number " +
                " FROM " +
                "   rentapp.juridical_persons LEFT OUTER JOIN  " +
                "   rentapp.districts ON " +
                " rentapp.juridical_persons.organization_district_id = rentapp.districts.id " +
                " LEFT OUTER JOIN " +
                "   rentapp.streets ON " +
                " rentapp.juridical_persons.organization_street_id = rentapp.streets.id " +
                " LEFT OUTER JOIN  " +
                "  rentapp.banks ON rentapp.juridical_persons.bank_id = rentapp.banks.id " +
                " GROUP BY " +
                "   rentapp.juridical_persons.id, " +
                " rentapp.juridical_persons.name_of_organization, " +
                "  rentapp.juridical_persons.director_surname,  " +
                " rentapp.juridical_persons.director_name, " +
                "  rentapp.juridical_persons.director_patronymic, " +
                "  rentapp.juridical_persons.organization_house_number, " +
                "   rentapp.juridical_persons.phone_number, " +
                "  rentapp.juridical_persons.payment_account, " +
                "  rentapp.juridical_persons.individual_taxpayer_number " +
                " ORDER BY    rentapp.juridical_persons.id;";
            IList<IJuridicalPersonDB> result = new List<IJuridicalPersonDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new JuridicalPersonDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    uint.Parse(row[5].ToString()),
                    row[6].ToString(),
                    row[7].ToString(),
                    row[8].ToString(),
                    row[9].ToString(),
                    row[10].ToString(),
                    row[11].ToString(),
                    row[12].ToString(),
                    row[13].ToString(),
                    row[14].ToString()
                ));
            }
            return result;
        }

        /// <summary>
        /// Изменение данных юридического лица из базы данных
        /// </summary>
        /// <param name="entity"></param>
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

        /// <summary>
        /// Удаление юридического лица из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from juridical_persons where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Получение полного имени руководителя по идентификатору договора
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IJuridicalPersonDB GetDirectorFullNameByContractId(uint contractId)
        {
            _query = $"SELECT director_surname, director_name, director_patronymic FROM rentapp.contracts " +
                $"join rentapp.juridical_persons on " +
                $" rentapp.contracts.juridical_person_id = rentapp.juridical_persons.id " +
                $"where rentapp.contracts.id = {contractId};";
            IJuridicalPersonDB result = null;
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            
            result = new JuridicalPersonDB(
                 dataTable.Rows[0][0].ToString(), dataTable.Rows[0][1].ToString(), dataTable.Rows[0][2].ToString()
            );
            
            return result;
        }
    }
}

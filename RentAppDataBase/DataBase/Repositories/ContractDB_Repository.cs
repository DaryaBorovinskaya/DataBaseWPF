using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с договорами из базы данных
    /// </summary>
    public class ContractDB_Repository :IRepositoryDB<IContractDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового договора в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IContractDB entity)
        {
            if (entity.IndividualId != null) 
                _query = $"insert into contracts " +
                     $"(individual_id, " +
                     $"employee_id, payment_frequency_id, registration_number, " +
                     $"begin_of_action, end_of_action, additional_conditions, fine) " +
                     $"values ({entity.IndividualId}, " +
                     $"{entity.EmployeeId}, {entity.PaymentFrequencyId}, " +
                     $"'{entity.RegistrationNumber}', '{entity.BeginOfAction}', " +
                     $"'{entity.EndOfAction}', '{entity.AdditionalConditions}', " +
                     $"{entity.Fine})";

            else if (entity.JuridicalPersonId != null)
            _query = $"insert into contracts " +
                     $"(juridical_person_id, " +
                     $"employee_id, payment_frequency_id, registration_number, " +
                     $"begin_of_action, end_of_action, additional_conditions, fine) " +
                     $"values ({entity.JuridicalPersonId}, " +
                     $"{entity.EmployeeId}, {entity.PaymentFrequencyId}, " +
                     $"'{entity.RegistrationNumber}', '{entity.BeginOfAction}', " +
                     $"'{entity.EndOfAction}', '{entity.AdditionalConditions}', " +
                     $"{entity.Fine})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        /// <summary>
        /// Чтение данных о договорах из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IContractDB> Read()
        {
            _query = $"SELECT  rentapp.contracts.id,   " +
                $" COALESCE(rentapp.contracts.individual_id, 0) AS individual_id, " +
                $" MAX(rentapp.individuals.surname) AS individual_surname, " +
                $" MAX(rentapp.individuals.name) AS individual_name, " +
                $" MAX(rentapp.individuals.patronymic) AS individual_patronymic, " +
                $" COALESCE(rentapp.contracts.juridical_person_id, 0) " +
                $" AS juridical_person_id,  " +
                $" MAX(rentapp.juridical_persons.name_of_organization) " +
                $" AS juridical_person_name,   " +
                $" COALESCE(rentapp.contracts.employee_id, 0) AS employee_id, " +
                $" MAX(rentapp.employees.surname) AS employee_surname,  " +
                $" MAX(rentapp.employees.name) AS employee_name,   " +
                $" MAX(rentapp.employees.patronymic) AS employee_patronymic,   " +
                $" COALESCE(rentapp.contracts.payment_frequency_id, 0) AS payment_frequency_id,   " +
                $" MAX(rentapp.payment_frequencies.title) AS payment_frequency_title,  " +
                $" rentapp.contracts.registration_number,  " +
                $" rentapp.contracts.begin_of_action,  " +
                $" rentapp.contracts.end_of_action,    rentapp.contracts.additional_conditions,  " +
                $"  rentapp.contracts.fine FROM   " +
                $"  rentapp.contracts LEFT OUTER JOIN   " +
                $"  rentapp.individuals ON rentapp.contracts.individual_id = rentapp.individuals.id LEFT OUTER JOIN   " +
                $"  rentapp.juridical_persons ON rentapp.contracts.juridical_person_id = rentapp.juridical_persons.id " +
                $" LEFT OUTER JOIN   " +
                $"  rentapp.employees ON rentapp.contracts.employee_id = rentapp.employees.id  LEFT OUTER JOIN   " +
                $"  rentapp.payment_frequencies ON rentapp.contracts.payment_frequency_id = rentapp.payment_frequencies.id " +
                $" GROUP BY   " +
                $"  rentapp.contracts.id,  " +
                $"  rentapp.contracts.registration_number, " +
                $"   rentapp.contracts.begin_of_action,   rentapp.contracts.end_of_action,  " +
                $"  rentapp.contracts.additional_conditions,\r\n   " +
                $" rentapp.contracts.fine\r\nORDER BY \r\n    rentapp.contracts.id;\r\n";

            IList<IContractDB> result = new List<IContractDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new ContractDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    uint.Parse(row[5].ToString()),
                    row[6].ToString(),
                    uint.Parse(row[7].ToString()),
                    row[8].ToString(),
                    row[9].ToString(),
                    row[10].ToString(),
                    uint.Parse(row[11].ToString()),
                    row[12].ToString(),
                    row[13].ToString(),
                    row[14].ToString().Substring(0, 10),
                    row[15].ToString().Substring(0, 10),
                    row[16].ToString(),
                    float.Parse(row[17].ToString())
                ));
            }
            return result;

        }

        /// <summary>
        /// Изменение данных договора из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IContractDB entity)
        {
            if (entity.IndividualId != null)
                _query = $"update contracts set " +
                     $"individual_id={entity.IndividualId}, " +
                     $"employee_id={entity.EmployeeId}, payment_frequency_id={entity.PaymentFrequencyId}, " +
                     $"registration_number='{entity.RegistrationNumber}', begin_of_action='{entity.BeginOfAction}', " +
                     $"end_of_action='{entity.EndOfAction}', additional_conditions='{entity.AdditionalConditions}', " +
                     $"fine={entity.Fine} " +
                     $"where id={entity.Id}";

            else if (entity.JuridicalPersonId != null)
                _query = $"update contracts set " +
                         $"juridical_person_id={entity.JuridicalPersonId}, " +
                         $"employee_id={entity.EmployeeId}, payment_frequency_id={entity.PaymentFrequencyId}, " +
                         $"registration_number='{entity.RegistrationNumber}', begin_of_action='{entity.BeginOfAction}', " +
                         $"end_of_action='{entity.EndOfAction}', additional_conditions='{entity.AdditionalConditions}', " +
                         $"fine={entity.Fine} " +
                         $"where id={entity.Id}";

            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление договора из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from contracts where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }



        /// <summary>
        /// Получение договора из базы данных по идентификатору физического лица
        /// </summary>
        /// <param name="individual_id"></param>
        /// <returns></returns>
        public IList<IContractDB> GetContractsByIndividualId(uint individual_id)
        {
            _query = $"SELECT  rentapp.contracts.id,   " +
                $" COALESCE(rentapp.contracts.individual_id, 0) AS individual_id, " +
                $" MAX(rentapp.individuals.surname) AS individual_surname, " +
                $" MAX(rentapp.individuals.name) AS individual_name, " +
                $" MAX(rentapp.individuals.patronymic) AS individual_patronymic, " +
                $" COALESCE(rentapp.contracts.juridical_person_id, 0) " +
                $" AS juridical_person_id,  " +
                $" MAX(rentapp.juridical_persons.name_of_organization) " +
                $" AS juridical_person_name,   " +
                $" COALESCE(rentapp.contracts.employee_id, 0) AS employee_id, " +
                $" MAX(rentapp.employees.surname) AS employee_surname,  " +
                $" MAX(rentapp.employees.name) AS employee_name,   " +
                $" MAX(rentapp.employees.patronymic) AS employee_patronymic,   " +
                $" COALESCE(rentapp.contracts.payment_frequency_id, 0) AS payment_frequency_id,   " +
                $" MAX(rentapp.payment_frequencies.title) AS payment_frequency_title,  " +
                $" rentapp.contracts.registration_number,  " +
                $" rentapp.contracts.begin_of_action,  " +
                $" rentapp.contracts.end_of_action,    rentapp.contracts.additional_conditions,  " +
                $"  rentapp.contracts.fine FROM   " +
                $"  rentapp.contracts LEFT OUTER JOIN   " +
                $"  rentapp.individuals ON rentapp.contracts.individual_id = rentapp.individuals.id LEFT OUTER JOIN   " +
                $"  rentapp.juridical_persons ON rentapp.contracts.juridical_person_id = rentapp.juridical_persons.id " +
                $" LEFT OUTER JOIN   " +
                $"  rentapp.employees ON rentapp.contracts.employee_id = rentapp.employees.id  LEFT OUTER JOIN   " +
                $"  rentapp.payment_frequencies ON rentapp.contracts.payment_frequency_id = rentapp.payment_frequencies.id " +
                $" where rentapp.contracts.individual_id = {individual_id} " +
                $" GROUP BY   " +
                $"  rentapp.contracts.id,  " +
                $"  rentapp.contracts.registration_number, " +
                $"   rentapp.contracts.begin_of_action,   rentapp.contracts.end_of_action,  " +
                $"  rentapp.contracts.additional_conditions,\r\n   " +
                $" rentapp.contracts.fine\r\nORDER BY \r\n    rentapp.contracts.id;\r\n";
            
            IList<IContractDB> result = new List<IContractDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new ContractDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    row[3].ToString(),
                    row[4].ToString(),
                    uint.Parse(row[5].ToString()),
                    row[6].ToString(),
                    uint.Parse(row[7].ToString()),
                    row[8].ToString(),
                    row[9].ToString(),
                    row[10].ToString(),
                    uint.Parse(row[11].ToString()),
                    row[12].ToString(),
                    row[13].ToString(),
                    row[14].ToString().Substring(0, 10),
                    row[15].ToString().Substring(0, 10),
                    row[16].ToString(),
                    float.Parse(row[17].ToString())
                ));
            }
            return result;
        }

        /// <summary>
        /// Получение договора из базы данных по идентификатору юридического лица
        /// </summary>
        /// <param name="juridicalPerson_id"></param>
        /// <returns></returns>
        public IList<IContractDB> GetContractsByJuridicalPersonId(uint juridicalPerson_id)
        {
            _query = $"SELECT  rentapp.contracts.id,   " +
                $" COALESCE(rentapp.contracts.individual_id, 0) AS individual_id, " +
                $" MAX(rentapp.individuals.surname) AS individual_surname, " +
                $" MAX(rentapp.individuals.name) AS individual_name, " +
                $" MAX(rentapp.individuals.patronymic) AS individual_patronymic, " +
                $" COALESCE(rentapp.contracts.juridical_person_id, 0) " +
                $" AS juridical_person_id,  " +
                $" MAX(rentapp.juridical_persons.name_of_organization) " +
                $" AS juridical_person_name,   " +
                $" COALESCE(rentapp.contracts.employee_id, 0) AS employee_id, " +
                $" MAX(rentapp.employees.surname) AS employee_surname,  " +
                $" MAX(rentapp.employees.name) AS employee_name,   " +
                $" MAX(rentapp.employees.patronymic) AS employee_patronymic,   " +
                $" COALESCE(rentapp.contracts.payment_frequency_id, 0) AS payment_frequency_id,   " +
                $" MAX(rentapp.payment_frequencies.title) AS payment_frequency_title,  " +
                $" rentapp.contracts.registration_number,  " +
                $" rentapp.contracts.begin_of_action,  " +
                $" rentapp.contracts.end_of_action,    rentapp.contracts.additional_conditions,  " +
                $"  rentapp.contracts.fine FROM   " +
                $"  rentapp.contracts LEFT OUTER JOIN   " +
                $"  rentapp.individuals ON rentapp.contracts.individual_id = rentapp.individuals.id LEFT OUTER JOIN   " +
                $"  rentapp.juridical_persons ON rentapp.contracts.juridical_person_id = rentapp.juridical_persons.id " +
                $" LEFT OUTER JOIN   " +
                $"  rentapp.employees ON rentapp.contracts.employee_id = rentapp.employees.id  LEFT OUTER JOIN   " +
                $"  rentapp.payment_frequencies ON rentapp.contracts.payment_frequency_id = rentapp.payment_frequencies.id " +
                $" where rentapp.contracts.juridical_person_id = {juridicalPerson_id} " +
                $" GROUP BY   " +
                $"  rentapp.contracts.id,  " +
                $"  rentapp.contracts.registration_number, " +
                $"   rentapp.contracts.begin_of_action,   rentapp.contracts.end_of_action,  " +
                $"  rentapp.contracts.additional_conditions,\r\n   " +
                $" rentapp.contracts.fine\r\nORDER BY \r\n    rentapp.contracts.id;\r\n";
            IList<IContractDB> result = new List<IContractDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new ContractDB(
                     uint.Parse(row[0].ToString()),
                     uint.Parse(row[1].ToString()),
                     row[2].ToString(),
                     row[3].ToString(),
                     row[4].ToString(),
                     uint.Parse(row[5].ToString()),
                     row[6].ToString(),
                     uint.Parse(row[7].ToString()),
                     row[8].ToString(),
                     row[9].ToString(),
                     row[10].ToString(),
                     uint.Parse(row[11].ToString()),
                     row[12].ToString(),
                     row[13].ToString(),
                     row[14].ToString().Substring(0, 10),
                     row[15].ToString().Substring(0, 10),
                     row[16].ToString(),
                     float.Parse(row[17].ToString())
                 ));
            }
            return result;
        }


    }
}

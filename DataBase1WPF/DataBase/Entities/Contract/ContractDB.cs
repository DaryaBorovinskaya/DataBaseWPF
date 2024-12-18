using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Contract
{
    public class ContractDB : IContractDB
    {
        public uint Id { get; set; }
        public uint? IndividualId { get; set; } 

        public string IndividualSurname { get; set; }

        public string IndividualName { get; set; }

        public string IndividualPatronymic { get; set; }
        public uint? JuridicalPersonId { get; set; }

        public string JuridicalPersonName { get; set; }
        public uint EmployeeId { get; set; }

        public string EmployeeSurname { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePatronymic { get; set; }
        public uint PaymentFrequencyId { get; set; }

        public string PaymentFrequencyTitle { get; set; }

        public string RegistrationNumber { get; set; }

        public string/*DateTime*/ BeginOfAction { get; set; }

        public string/*DateTime*/ EndOfAction { get; set; }

        public string AdditionalConditions { get; set; }

        public float Fine { get; set; }

        public ContractDB(uint id, uint individualId, 
            string individualSurname, string individualName, string individualPatronymic,
            uint juridicalPersonId, string juridicalPersonName,
            uint employeeId, string employeeSurname, string employeeName, string employeePatronymic,
            uint paymentFrequencyId, string paymentFrequencyTitle, string registrationNumber,
            string/*DateTime*/ beginOfAction, string/*DateTime*/ endOfAction, string additionalConditions, 
            float fine)
        {
            Id = id;
            IndividualId = individualId;
            IndividualSurname = individualSurname;
            IndividualName = individualName;
            IndividualPatronymic = individualPatronymic;
            JuridicalPersonId = juridicalPersonId;
            JuridicalPersonName = juridicalPersonName;
            EmployeeId = employeeId;
            EmployeeSurname = employeeSurname;
            EmployeeName = employeeName;
            EmployeePatronymic = employeePatronymic;
            PaymentFrequencyId = paymentFrequencyId;
            PaymentFrequencyTitle = paymentFrequencyTitle;
            RegistrationNumber = registrationNumber;
            BeginOfAction = beginOfAction;
            EndOfAction = endOfAction;
            AdditionalConditions = additionalConditions;
            Fine = fine;
        }


        public ContractDB(uint id, uint? individualId, uint? juridicalPersonId,
            uint employeeId, uint paymentFrequencyId, string registrationNumber,
            string/*DateTime*/ beginOfAction, string/*DateTime*/ endOfAction, string additionalConditions,
            float fine)
        {
            Id = id;
            IndividualId = individualId;
            JuridicalPersonId = juridicalPersonId;
            EmployeeId = employeeId;
            PaymentFrequencyId = paymentFrequencyId;
            RegistrationNumber = registrationNumber;
            BeginOfAction = beginOfAction;
            EndOfAction = endOfAction;
            AdditionalConditions = additionalConditions;
            Fine = fine;
        }

        public ContractDB(uint? individualId, uint? juridicalPersonId,
            uint employeeId, uint paymentFrequencyId, string registrationNumber,
            string beginOfAction, string endOfAction, string additionalConditions,
            float fine)
        {
            IndividualId = individualId;
            JuridicalPersonId = juridicalPersonId;
            EmployeeId = employeeId;
            PaymentFrequencyId = paymentFrequencyId;
            RegistrationNumber = registrationNumber;
            BeginOfAction = beginOfAction;
            EndOfAction = endOfAction;
            AdditionalConditions = additionalConditions;
            Fine = fine;
        }
    }
}

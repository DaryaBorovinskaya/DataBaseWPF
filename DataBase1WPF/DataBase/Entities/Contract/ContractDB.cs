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
        public uint IndividualId { get; set; }
        public uint JuridicalPersonId { get; set; }
        public uint EmployeeId { get; set; }
        public uint PaymentFrequencyId { get; set; }

        public string RegistrationNumber { get; set; }

        public DateOnly BeginOfAction { get; set; }

        public DateOnly EndOfAction { get; set; }

        public string AdditionalConditions { get; set; }

        public float Fine { get; set; }

        public ContractDB(uint id, uint individualId, uint juridicalPersonId, 
            uint employeeId, uint paymentFrequencyId, string registrationNumber, 
            DateOnly beginOfAction, DateOnly endOfAction, string additionalConditions, 
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

        public ContractDB(uint individualId, uint juridicalPersonId,
            uint employeeId, uint paymentFrequencyId, string registrationNumber,
            DateOnly beginOfAction, DateOnly endOfAction, string additionalConditions,
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

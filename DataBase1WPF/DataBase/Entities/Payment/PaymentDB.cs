using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Payment
{
    public class PaymentDB : IPaymentDB
    {
        public uint Id { get; set; }
        public uint IndividualId { get; set; }
        public uint JuridicalPersonId { get; set; }
        public uint ContractId { get; set; }

        public DateOnly DateOfPayment { get; set; }

        public float AmountOfPayment { get; set; }

        public PaymentDB(uint id, uint individualId, uint juridicalPersonId, 
            uint contractId, DateOnly dateOfPayment, float amountOfPayment)
        {
            Id = id;
            IndividualId = individualId;
            JuridicalPersonId = juridicalPersonId;
            ContractId = contractId;
            DateOfPayment = dateOfPayment;
            AmountOfPayment = amountOfPayment;
        }

        public PaymentDB(uint individualId, uint juridicalPersonId, uint contractId, 
            DateOnly dateOfPayment, float amountOfPayment)
        {
            IndividualId = individualId;
            JuridicalPersonId = juridicalPersonId;
            ContractId = contractId;
            DateOfPayment = dateOfPayment;
            AmountOfPayment = amountOfPayment;
        }
    }
}

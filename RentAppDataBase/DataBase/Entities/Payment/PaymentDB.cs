using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Payment
{
    /// <summary>
    /// Данные о платежах по договорам из базы данных
    /// </summary>
    public class PaymentDB : IPaymentDB
    {
        public uint Id { get; set; }
        public uint ContractId { get; set; }

        public string DateOfPayment { get; set; }

        public float AmountOfPayment { get; set; }

        public PaymentDB(uint id, 
            uint contractId,
            string dateOfPayment, float amountOfPayment)
        {
            Id = id;
            ContractId = contractId;
            DateOfPayment = dateOfPayment;
            AmountOfPayment = amountOfPayment;
        }


        
        public PaymentDB(uint contractId, string dateOfPayment, 
            float amountOfPayment)
        {
            ContractId = contractId;
            DateOfPayment = dateOfPayment;
            AmountOfPayment = amountOfPayment;
        }
    }
}

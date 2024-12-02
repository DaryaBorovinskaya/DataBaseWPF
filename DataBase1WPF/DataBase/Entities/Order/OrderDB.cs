using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Order
{
    public class OrderDB : IOrderDB
    {
        public uint Id { get; set; }
        public uint PremiseID { get; set; }
        public uint RentalPurposeId { get; set; }
        public uint ContractId { get; set; }

        public DateTime BeginOfRent { get; set; }

        public DateTime EndOfRent { get; set; }

        public float RentalPayment { get; set; }

        public OrderDB(uint id, uint premiseID, uint rentalPurposeId, 
            uint contractId, DateTime beginOfRent, DateTime endOfRent, 
            float rentalPayment)
        {
            Id = id;
            PremiseID = premiseID;
            RentalPurposeId = rentalPurposeId;
            ContractId = contractId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }

        public OrderDB(uint premiseID, uint rentalPurposeId,
            uint contractId, DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            PremiseID = premiseID;
            RentalPurposeId = rentalPurposeId;
            ContractId = contractId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }
    }
}

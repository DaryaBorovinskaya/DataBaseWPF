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
        public uint ContractId { get; set; }
        public uint PremiseID { get; set; }
        
        public uint DistrictId { get; set; }

        [DisplayName("Район")]
        public string DistrictTitle { get; set; }

        public uint StreetId { get; set; }
        [DisplayName("Улица")]
        public string StreetTitle { get; set; }

        public uint BuildingId { get; set; }

        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }


        [DisplayName("Номер этажа")]
        public int FloorNumber { get; set; }

        [DisplayName("Номер помещения")]
        public string PremiseNumber { get; set; }

        [DisplayName("Площадь")]
        public float Area { get; set; }


        public uint RentalPurposeId { get; set; }

        [DisplayName("Цель аренды")]
        public string RentalPurposeTitle { get; set; }


        [DisplayName("Начало срока аренды")]
        public string BeginOfRent { get; set; }

        [DisplayName("Конец срока аренды")]
        public string EndOfRent { get; set; }

        [DisplayName("Арендная плата")]
        public float RentalPayment { get; set; }

        public OrderDB(uint id, uint contractId, uint premiseID, uint districtId, 
            string districtTitle,
            uint streetId, string streetTitle, 
            uint buildingId, string houseNumber, int floorNumber, string premiseNumber,
            float area, uint rentalPurposeId,  string rentalPurposeTitle,
             string beginOfRent, string endOfRent, 
            float rentalPayment)
        {
            Id = id;
            ContractId = contractId;
            PremiseID = premiseID;
            DistrictId = districtId;
            DistrictTitle = districtTitle;
            StreetId = streetId;
            StreetTitle = streetTitle;
            BuildingId = buildingId;
            HouseNumber = houseNumber;
            FloorNumber = floorNumber;
            PremiseNumber = premiseNumber;
            Area = area;
            RentalPurposeId = rentalPurposeId;
            RentalPurposeTitle = rentalPurposeTitle;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }

        public OrderDB(uint id, uint contractId, uint premiseID, uint districtId,
            uint streetId, 
            uint buildingId, string houseNumber, int floorNumber, string premiseNumber,
            float area, uint rentalPurposeId, 
             string beginOfRent, string endOfRent,
            float rentalPayment)
        {
            Id = id;
            ContractId = contractId;
            PremiseID = premiseID;
            DistrictId = districtId;
            StreetId = streetId;
            BuildingId = buildingId;
            HouseNumber = houseNumber;
            FloorNumber = floorNumber;
            PremiseNumber = premiseNumber;
            Area = area;
            RentalPurposeId = rentalPurposeId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }



        public OrderDB( uint contractId, uint premiseID, uint districtId,
            uint streetId,
            uint buildingId, string houseNumber, int floorNumber, string premiseNumber,
            float area, uint rentalPurposeId,
             string beginOfRent, string endOfRent,
            float rentalPayment)
        {
            ContractId = contractId;
            PremiseID = premiseID;
            DistrictId = districtId;
            StreetId = streetId;
            BuildingId = buildingId;
            HouseNumber = houseNumber;
            FloorNumber = floorNumber;
            PremiseNumber = premiseNumber;
            Area = area;
            RentalPurposeId = rentalPurposeId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }


        public OrderDB(uint id,  uint premiseID, uint rentalPurposeId, uint contractId,
             string beginOfRent, string endOfRent,
            float rentalPayment)
        {
            Id = id;
            ContractId = contractId;
            PremiseID = premiseID;
            RentalPurposeId = rentalPurposeId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }


        public OrderDB(uint contractId, uint premiseID, uint rentalPurposeId,
            string beginOfRent, string endOfRent,
            float rentalPayment)
        {
            ContractId = contractId;
            PremiseID = premiseID;
            RentalPurposeId = rentalPurposeId;
            BeginOfRent = beginOfRent;
            EndOfRent = endOfRent;
            RentalPayment = rentalPayment;
        }
    }
}

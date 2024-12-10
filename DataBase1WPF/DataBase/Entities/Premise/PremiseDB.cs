using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Premise
{
    public class PremiseDB : IPremiseDB
    {
        public uint Id { get; set; }
        public uint BuildingID { get; set; }
        public uint TypeOfFinishingId { get; set; }
        public string TypeOfFinishingTitle { get; set; }

        public string PremiseNumber { get; set; }

        public float Area { get; set; }

        public int FloorNumber { get; set; }

        public bool AvailabilityOfPhoneNumber { get; set; }

        public float TempRentalPayment { get; set; }

        public PremiseDB(uint id, uint buildingID, uint typeOfFinishingId, 
            string typeOfFinishingTitle,
            string premiseNumber, float area, int floorNumber, 
            bool availabilityOfPhoneNumber, float tempRentalPayment)
        { 
            Id = id;
            BuildingID = buildingID;
            TypeOfFinishingId = typeOfFinishingId;
            TypeOfFinishingTitle = typeOfFinishingTitle;
            PremiseNumber = premiseNumber;
            Area = area;
            FloorNumber = floorNumber;
            AvailabilityOfPhoneNumber = availabilityOfPhoneNumber;
            TempRentalPayment = tempRentalPayment;
        }

        public PremiseDB(uint buildingID, uint typeOfFinishingId,
            string typeOfFinishingTitle,
            string premiseNumber, float area, int floorNumber,
            bool availabilityOfPhoneNumber, float tempRentalPayment)
        {
            BuildingID = buildingID;
            TypeOfFinishingId = typeOfFinishingId;
            TypeOfFinishingTitle = typeOfFinishingTitle;
            PremiseNumber = premiseNumber;
            Area = area;
            FloorNumber = floorNumber;
            AvailabilityOfPhoneNumber = availabilityOfPhoneNumber;
            TempRentalPayment = tempRentalPayment;
        }
    }
}

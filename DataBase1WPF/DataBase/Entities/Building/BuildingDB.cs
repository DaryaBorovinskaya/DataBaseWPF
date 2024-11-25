using System.ComponentModel;

namespace DataBase1WPF.DataBase.Entities.Building
{
    public class BuildingDB :IBuildingDB
    {
        public uint Id { get; set; }
        public uint DistrictId { get; set; }
        public uint StreetId { get; set; }
        public string HouseNumber { get; set; }
        public uint NumberOfFloors { get; set; }
        public uint CountRentalPremises { get; set; }
        public string CommandantPhoneNumber { get; set; }

        public BuildingDB(uint id, uint districtId, uint streetId, string houseNumber, 
            uint numberOfFloors, uint countRentalPremises, string commandantPhoneNumber)
        {
            Id = id;
            DistrictId = districtId;
            StreetId = streetId;
            HouseNumber = houseNumber;
            NumberOfFloors = numberOfFloors;
            CountRentalPremises = countRentalPremises;
            CommandantPhoneNumber = commandantPhoneNumber;
        }

        public BuildingDB(uint districtId, uint streetId, string houseNumber,
            uint numberOfFloors, uint countRentalPremises, string commandantPhoneNumber)
        {
            DistrictId = districtId;
            StreetId = streetId;
            HouseNumber = houseNumber;
            NumberOfFloors = numberOfFloors;
            CountRentalPremises = countRentalPremises;
            CommandantPhoneNumber = commandantPhoneNumber;
        }
    }
}

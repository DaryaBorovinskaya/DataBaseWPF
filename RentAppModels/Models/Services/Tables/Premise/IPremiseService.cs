using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Premise
{
    public interface IPremiseService
    {
        public DataTable? GetPremisesByBuilding(uint id);
        public DataTable SearchDataInTable(uint buildingId, string searchLine);
        public List<string> GetTypesOfFinishing();

        public int GetTypeOfFinishingSelectedIndex(DataRow row);

        public void Add(uint buildingId, int typeOfFinishingIndex, string premiseNumber,
            float area, int floorNumber, bool availAbilityOfPhoneNumber,
            float tempRentalPayment);

        public void Update(DataRow row, uint buildingId, int typeOfFinishingIndex, string premiseNumber,
            float area, int floorNumber, bool availAbilityOfPhoneNumber,
            float tempRentalPayment);

        public void Delete(DataRow row);
    }
}

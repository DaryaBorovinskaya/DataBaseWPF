using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Building
{
    public interface IBuildingService
    {
        public DataTable? GetPremisesByBuilding(DataRow row);
        public string GetOtherTableName();
    }
}

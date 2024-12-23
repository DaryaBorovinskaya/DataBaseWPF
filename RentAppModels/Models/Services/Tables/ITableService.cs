using System.Data;

namespace DataBase1WPF.Models.Services.Tables
{
    public interface ITableService
    {
        public DataTable GetValuesTable();
        public string GetTableName();

        public DataTable SearchDataInTable(string searchLine);

        public UserAbilitiesType GetUserAbilities(uint menuElemId);
        public void Delete(DataRow row);

    }
}

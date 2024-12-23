using System.Data;

namespace DataBase1WPF.Models.Services.Tables.WorkRecordCard
{
    public interface IWorkRecordCardService
    {
        public DataTable? GetWorkRecordCardByEmployee(uint id);
        public DataTable SearchDataInTable(uint buildingId, string searchLine);
        public List<string> GetPositions();

        public int GetPositionsSelectedIndex(DataRow row);

        public void Add(uint employeeId, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording);

        public void Update(DataRow row, uint employeeId, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording);

        public void Delete(DataRow row);
    }
}

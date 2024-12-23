using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.WorkRecordCard
{
    public class WorkRecordCardService : IWorkRecordCardService
    {
        private Dictionary<DataRow, IWorkRecordCardDB> _dataDictionary;
        public DataTable? GetWorkRecordCardByEmployee(uint id)
        {
            DataTable table = null;
            if (DataManager.GetInstance().WorkRecordCardDB_Repository is WorkRecordCardDB_Repository repository)
            {
                List<IWorkRecordCardDB> values = repository.GetWorkRecordCardByEmployeeId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }

        public DataTable SearchDataInTable(uint employeeId, string searchLine)
        {
            DataTable table = new();
            if (DataManager.GetInstance().WorkRecordCardDB_Repository is WorkRecordCardDB_Repository repository)
            {
                List<IWorkRecordCardDB> values = repository.GetWorkRecordCardByEmployeeId(employeeId).ToList().Where(
                    item => item.PositionName.Contains(searchLine)
                    || item.OrderNumber.Contains(searchLine) || item.OrderDate.ToString().Contains(searchLine)
                    || item.ReasonOfRecording.ToString().Contains(searchLine)).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);

            }
            return table;
        }


        public List<string> GetPositions()
        {
            List<string> positions = new();

            List<IPositionDB> positionsDB = DataManager.GetInstance().PositionDB_Repository.Read().ToList();

            foreach (IPositionDB positionDB in positionsDB)
                positions.Add(positionDB.Name);


            return positions;
        }

        public int GetPositionsSelectedIndex(DataRow row)
        {
            List<IPositionDB> positions = DataManager.GetInstance().PositionDB_Repository.Read().ToList();

            return positions.FindIndex((elem) => elem.Id == _dataDictionary[row].PositionId);
        }



        public void Add(uint employeeId, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            DataManager.GetInstance().WorkRecordCardDB_Repository.Create(new WorkRecordCardDB(
                employeeId,
                DataManager.GetInstance().PositionDB_Repository.Read().ToList()[positionIndex].Id,
                orderNumber,
                orderDate.ToString("yyyy-MM-dd"),
                reasonOfRecording
                ));
        }

        public void Update(DataRow row, uint employeeId, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            DataManager.GetInstance().WorkRecordCardDB_Repository.Update(new WorkRecordCardDB(
                _dataDictionary[row].Id,
                employeeId,
                DataManager.GetInstance().PositionDB_Repository.Read().ToList()[positionIndex].Id,
                orderNumber,
                orderDate.ToString("yyyy-MM-dd"),
                reasonOfRecording
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().WorkRecordCardDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.WorkRecordCard
{
    /// <summary>
    /// Сервис для записей трудовой книжки
    /// </summary>
    public class WorkRecordCardService : IWorkRecordCardService
    {
        private Dictionary<DataRow, IWorkRecordCardDB> _dataDictionary;

        /// <summary>
        /// Получение записей трудовой книжки по идентификатору сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Поиск данных по таблице записей в трудовой книжке
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <returns></returns>
        public List<string> GetPositions()
        {
            List<string> positions = new();

            List<IPositionDB> positionsDB = DataManager.GetInstance().PositionDB_Repository.Read().ToList();

            foreach (IPositionDB positionDB in positionsDB)
                positions.Add(positionDB.Name);


            return positions;
        }

        /// <summary>
        /// Получение индекса должности у выбранной записи в трудовой книжке
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetPositionsSelectedIndex(DataRow row)
        {
            List<IPositionDB> positions = DataManager.GetInstance().PositionDB_Repository.Read().ToList();

            return positions.FindIndex((elem) => elem.Id == _dataDictionary[row].PositionId);
        }


        /// <summary>
        /// Добавление записи в трудовую книжку
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="positionIndex"></param>
        /// <param name="orderNumber"></param>
        /// <param name="orderDate"></param>
        /// <param name="reasonOfRecording"></param>
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

        /// <summary>
        /// Изменение данных записи в трудовой книжке
        /// </summary>
        /// <param name="row"></param>
        /// <param name="employeeId"></param>
        /// <param name="positionIndex"></param>
        /// <param name="orderNumber"></param>
        /// <param name="orderDate"></param>
        /// <param name="reasonOfRecording"></param>
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

        /// <summary>
        /// Удаление записи из трудовой книжки
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().WorkRecordCardDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class WorkRecordCardDB_Repository : IRepositoryDB<IWorkRecordCardDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IWorkRecordCardDB entity)
        {
            _query = $"insert into work_record_cards " +
                     $"(employee_id, position_id, order_number," +
                     $"order_date, reason_of_recording) " +
                     $"values ({entity.EmployeeID}, {entity.PositionId}," +
                     $"'{entity.OrderNumber}', {entity.OrderDate}, '{entity.ReasonOfRecording}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IWorkRecordCardDB> Read()
        {
            _query = "select * from work_record_cards";
            IList<IWorkRecordCardDB> result = new List<IWorkRecordCardDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new WorkRecordCardDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    DateOnly.Parse(row[4].ToString()),
                    row[5].ToString()
                ));
            }
            return result;
        }

        public void Update(IWorkRecordCardDB entity)
        {
            _query = $"update work_record_cards set " +
                     $"employee_id={entity.EmployeeID}, position_id={entity.PositionId}, " +
                     $"order_number='{entity.OrderNumber}', " +
                     $"order_date={entity.OrderDate}, reason_of_recording='{entity.ReasonOfRecording}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IWorkRecordCardDB entity)
        {
            _query = $"delete from work_record_cards where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

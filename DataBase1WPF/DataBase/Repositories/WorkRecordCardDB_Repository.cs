using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Premise;
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
                     $"'{entity.OrderNumber}', '{entity.OrderDate.ToString("yyyy-MM-dd")}', '{entity.ReasonOfRecording}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IWorkRecordCardDB> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(IWorkRecordCardDB entity)
        {
            _query = $"update work_record_cards set " +
                     $"employee_id={entity.EmployeeID}, position_id={entity.PositionId}, " +
                     $"order_number='{entity.OrderNumber}', " +
                     $"order_date='{entity.OrderDate.ToString("yyyy-MM-dd")}', reason_of_recording='{entity.ReasonOfRecording}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from work_record_cards where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        public  IList<IWorkRecordCardDB> GetWorkRecordCardByEmployeeId(uint employee_id)
        {
            _query = $"SELECT  rentapp.work_record_cards.id, rentapp.work_record_cards.employee_id, " +
                $" COALESCE(rentapp.work_record_cards.position_id, 0) AS position_id, " +
                $" MAX(rentapp.positions.name) AS position_name,  rentapp.work_record_cards.order_number, " +
                $" rentapp.work_record_cards.order_date, " +
                $" rentapp.work_record_cards.reason_of_recording " +
                $" FROM   rentapp.work_record_cards " +
                $" left outer join rentapp.employees " +
                $" on rentapp.work_record_cards.employee_id =  rentapp.employees.id " +
                $" LEFT OUTER JOIN rentapp.positions ON " +
                $" rentapp.work_record_cards.position_id = rentapp.positions.id " +
                $" where rentapp.employees.id = {employee_id} " +
                $" GROUP BY rentapp.work_record_cards.id,  rentapp.work_record_cards.order_number, " +
                $" rentapp.work_record_cards.order_date, " +
                "  rentapp.work_record_cards.reason_of_recording ORDER BY rentapp.work_record_cards.id;";
            IList<IWorkRecordCardDB> result = new List<IWorkRecordCardDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new WorkRecordCardDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    row[4].ToString(),
                    DateTime.Parse(row[5].ToString()),
                    row[6].ToString()
                ));
            }
            return result;
        }
    }
}

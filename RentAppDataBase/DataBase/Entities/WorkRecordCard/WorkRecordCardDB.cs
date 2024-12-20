using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.WorkRecordCard
{
    public class WorkRecordCardDB : IWorkRecordCardDB
    {
        public uint Id { get; set; }
        public uint EmployeeID { get; set; }
        public uint PositionId { get; set; }
        

        public string OrderNumber { get; set; }

        public string/*DateTime*/ OrderDate { get; set; }
        public string PositionName { get; set; }

        public string ReasonOfRecording { get; set; }
        
        public WorkRecordCardDB(uint id, uint employeeID, uint positionId, 
            string positionName, string orderNumber, string /*DateTime*/ orderDate, 
            string reasonOfRecording)
        {
            Id = id;
            EmployeeID = employeeID;
            PositionId = positionId;
            PositionName = positionName;
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            ReasonOfRecording = reasonOfRecording;
        }

        public WorkRecordCardDB(uint id, uint employeeID, uint positionId,
            string orderNumber, string /*DateTime*/ orderDate, string reasonOfRecording)
        {
            Id = id;
            EmployeeID = employeeID;
            PositionId = positionId;
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            ReasonOfRecording = reasonOfRecording;
        }


        public WorkRecordCardDB(uint employeeID, uint positionId,
            string orderNumber, string /*DateTime*/ orderDate, string reasonOfRecording)
        {
            EmployeeID = employeeID;
            PositionId = positionId;
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            ReasonOfRecording = reasonOfRecording;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.WorkRecordCard
{
    public interface IWorkRecordCardDB
    {
        public uint Id { get; set; }
        public uint EmployeeID { get; set; }
        public uint PositionId { get; set; }

        

        [DisplayName("Номер приказа")]
        public string OrderNumber { get; set; }

        [DisplayName("Дата приказа")]
        public string OrderDate { get; set; } 

        [DisplayName("Должность")]
        public string PositionName { get; set; }

        [DisplayName("Причина записи")]
        public string ReasonOfRecording { get; set; }

        
    }
}

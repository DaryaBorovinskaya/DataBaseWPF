using DataBase1WPF.DataBase.Entities.Fine;
using DataBase1WPF.DataBase.Entities.Position;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class PositionsService : ITableName, ITableService
    {
        private IList<IPositionDB> GetValues()
        {
            List<IPositionDB> values = DataManager.GetInstance().PositionDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues());
            table.Columns.Remove(table.Columns[0]);
            return table;
        }

        public string GetTableName()
        {
            return "Должности";
        }
    }
}

using DataBase1WPF.DataBase.Entities.Fine;
using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class FineService : ITableName, ITableService
    {
        private List<IFineDB> GetValues()
        {
            List<IFineDB> values = DataManager.GetInstance().FineDB_Repository.Read().ToList();
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
            return "Штраф";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues().Where(item => item.Amount.ToString().Contains(searchLine)).ToList());
            table.Columns.Remove(table.Columns[0]);
            return table;
        }
    }
}

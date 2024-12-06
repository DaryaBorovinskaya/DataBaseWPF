using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class BanksService :  ITableName, ITableService
    {
        private IList<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().BankDB_Repository.Read().ToList();
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
            return "Банки";
        }
    }
}

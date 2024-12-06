using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class RentalPurposesService : IHandbooksService<IHandbookDB>, ITableName
    {
        public IList<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable(IList<IHandbookDB> values)
        {
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            return table;
        }
        public string GetTableName()
        {
            return "Цели аренды";
        }
    }
}

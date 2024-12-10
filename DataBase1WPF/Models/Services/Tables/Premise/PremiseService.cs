using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Premise
{
    public class PremiseService : IPremiseService
    {
        public DataTable? GetPremisesByBuilding(uint id)
        {
            DataTable table = null;
            if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
            {
                List<IPremiseDB> values = repository.GetPremisesByBuildingId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
            }
            return table;
        }
    }
}

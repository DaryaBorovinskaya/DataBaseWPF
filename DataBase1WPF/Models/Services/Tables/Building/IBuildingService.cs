using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Building
{
    public interface IBuildingService
    {
        public DataTable? GetPremisesByBuilding(DataRow row);
        public string GetOtherTableName();
    }
}

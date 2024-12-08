using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables
{
    public interface ITableService
    {
        public DataTable GetValuesTable();
        public string GetTableName();

        public DataTable SearchDataInTable(string searchLine);

        public UserAbilitiesType GetUserAbilities(uint menuElemId);
        public void Delete(int selectedIndex);

    }
}

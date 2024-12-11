using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Premise
{
    public interface IPremiseService
    {
        public DataTable? GetPremisesByBuilding(uint id);
        public DataTable SearchDataInTable(string searchLine);
        public void Add(string title);

        public void Update(DataRow row, string title);

        public void Delete(DataRow row);
    }
}

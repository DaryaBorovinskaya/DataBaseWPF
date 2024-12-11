using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Handbook;
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
        private Dictionary<DataRow, IPremiseDB> _dataDictionary;
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

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }

        public DataTable SearchDataInTable(string searchLine)
        {
            throw new NotImplementedException();
        }

        public void Add(string title)
        {
            DataManager.GetInstance().BankDB_Repository.Create(new HandbookDB(title));
        }

        public void Update(DataRow row, string title)
        {
            DataManager.GetInstance().BankDB_Repository.Update(new HandbookDB(_dataDictionary[row].Id, title));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PremiseDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    internal class MenuElemDB_Repository : IRepositoryDB<IMenuElemDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IMenuElemDB entity)
        {
            throw new NotImplementedException();
        }

        public IList<IMenuElemDB> Read()
        {
            _query = "select * from menu_elements";
            IList<IMenuElemDB> result = new List<IMenuElemDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new MenuElemDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    row[3]?.ToString(),
                    row[4]?.ToString(),
                    uint.Parse(row[5].ToString())
                ));
            }
            return result;
        }

        public void Update(IMenuElemDB entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(uint id)
        {
            throw new NotImplementedException();
        }
    }
}

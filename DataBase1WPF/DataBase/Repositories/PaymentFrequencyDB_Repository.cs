﻿using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    class PaymentFrequencyDB_Repository : IRepositoryDB<IHandbookDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IHandbookDB entity)
        {
            _query = $"insert into payment_frequencies " +
                     $"(title) " +
                     $"values ('{entity.Title}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IHandbookDB> Read()
        {
            _query = "select * from payment_frequencies";
            IList<IHandbookDB> result = new List<IHandbookDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new HandbookDB(
                    uint.Parse(row[0].ToString()),
                    row[1].ToString()
                ));
            }
            return result;
        }

        public void Update(IHandbookDB entity)
        {
            _query = $"update payment_frequencies set " +
                     $"title='{entity.Title}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(IHandbookDB entity)
        {
            _query = $"delete from payment_frequencies where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}
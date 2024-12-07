﻿using DataBase1WPF.DataBase.Entities.Handbook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class StreetsService : ITableService
    {
        private List<IHandbookDB> GetValues()
        {
            List<IHandbookDB> values = DataManager.GetInstance().StreetDB_Repository.Read().ToList();
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
            return "Улицы";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = DataTableConverter.ToDataTable(GetValues().Where(item => item.Title.Contains(searchLine)).ToList());
            table.Columns.Remove(table.Columns[0]);
            return table;
        }

        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            throw new NotImplementedException();
        }
    }
}

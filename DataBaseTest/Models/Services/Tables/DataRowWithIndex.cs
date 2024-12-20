using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables
{
    public class DataRowWithIndex
    {
        public DataRow DataRow { get; set; }
        public int Index { get; set; }

        public DataRowWithIndex(DataRow dataRow, int index)
        {
            DataRow = dataRow;
            Index = index;
        }
    }
}

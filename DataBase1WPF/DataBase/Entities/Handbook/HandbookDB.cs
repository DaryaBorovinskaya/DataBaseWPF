using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Handbook
{
    class HandbookDB : IHandbookDB
    {
        public uint Id { get; set; }
        public string Title { get; set; }

        public HandbookDB(uint id, string title)
        {
            Id = id;
            Title = title;
        }

        public HandbookDB(string title)
        {
            Title = title;
        }
    }
}

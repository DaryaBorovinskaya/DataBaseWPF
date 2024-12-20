using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBase1WPF.DataBase.Entities.Handbook
{
    public class HandbookDB : IHandbookDB, IComparable
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

        public int CompareTo(object? obj)
        {
            if ((obj == null) || (!(obj is HandbookDB)))
                return 0;
            else
                return string.Compare(Title, ((HandbookDB)obj).Title);
        }
    }
}

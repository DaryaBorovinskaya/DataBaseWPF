using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Handbook
{
    public interface IHandbookDB
    {
        public uint Id { get; set; }

        [DisplayName("Наименование")]
        public string Title { get; set; }
    }
}

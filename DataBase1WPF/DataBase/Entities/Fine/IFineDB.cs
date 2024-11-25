using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Fine
{
    public interface IFineDB
    {
        public uint Id { get; set; }

        [DisplayName("Сумма")]
        public float Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Position
{
    public interface IPositionDB
    {
        uint Id { get; set; }

        [DisplayName("Наименование")]
        string Name { get; set; }

        [DisplayName("Оклад")]
        float Salary { get; set; }
    }
}

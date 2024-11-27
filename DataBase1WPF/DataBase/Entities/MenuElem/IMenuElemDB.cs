using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.MenuElem
{
    public interface IMenuElemDB
    {
        public uint Id { get; set; }
        public uint ParentId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Имя DLL")]
        public string DllName { get; set; }

        [DisplayName("Имя функции")]
        public string FuncName { get; set; }

        [DisplayName("Порядок")]
        public uint Order { get; set; }
    }
}

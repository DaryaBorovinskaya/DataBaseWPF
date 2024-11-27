using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.MenuElem
{
    public class MenuElemDB : IMenuElemDB
    {
        public uint Id { get; set; }
        public uint ParentId { get; set; }
        public string Name { get; set; }
        public string DllName { get; set; }
        public string FuncName { get; set; }
        public uint Order { get; set; }

        public MenuElemDB(uint id, uint parentId, string name, string dllName, 
            string funcName, uint order)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            DllName = dllName;
            FuncName = funcName;
            Order = order;
        }

        public MenuElemDB(uint parentId, string name, string dllName,
            string funcName, uint order)
        {
            ParentId = parentId;
            Name = name;
            DllName = dllName;
            FuncName = funcName;
            Order = order;
        }
    }
}

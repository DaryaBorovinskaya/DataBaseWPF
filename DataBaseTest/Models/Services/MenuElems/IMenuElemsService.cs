using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.MenuElems
{
    public interface IMenuElemsService
    {
        public uint GetCurrentMenuElemId(string functionName);
    }
}

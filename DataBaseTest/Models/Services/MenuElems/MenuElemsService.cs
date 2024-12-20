using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.MenuElems
{
    public class MenuElemsService : IMenuElemsService
    {
        public uint GetCurrentMenuElemId(string functionName)
        {
            return DataManager.GetInstance().MenuElemDB_Repository.Read().ToList().Where(
                elem => elem.FuncName == functionName
                ).ToList()[0].Id;
            
        }
    }
}

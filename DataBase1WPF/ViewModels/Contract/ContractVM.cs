using DataBase1WPF.Models.Services.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.ViewModels.Contract
{
    public class ContractVM : BasicVM
    {
        private ITableService _tableService;
        private DataRow _row;

        public ContractVM(DataRow row, ITableService tableService)
        {
            _tableService = tableService;
            _row = row;
        }
    }
}

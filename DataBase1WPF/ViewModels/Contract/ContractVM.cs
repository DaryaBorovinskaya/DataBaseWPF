using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.Models.Services.Tables.Individual;
using DataBase1WPF.Models.Services.Tables.JuridicalPerson;
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
        private ITableService _clientService;
        private DataRow _row;
        private string _windowTitle;
        private DataTable _dataTableContracts;



        public string WindowTitle
        {
            get { return _windowTitle; }
        }

        public DataTable DataTableContracts
        {
            get { return _dataTableContracts; }
            set
            {
                if (_dataTableContracts == null)
                    _dataTableContracts = value;
                else
                    Set(ref _dataTableContracts, value);
            }
        }


        public ContractVM(DataRow row, ITableService clientService,
            ITableService tableService, uint client_id)
        {
            _tableService = tableService;
            _row = row;
            _clientService = clientService;
            
            if (_clientService is IndividualService service)
            {
                _windowTitle = $"Договоры клиента: {row[0].ToString() + " "
                + row[1].ToString() + " " + row[2].ToString() + " "}";
            }
            else if (_clientService is JuridicalPersonService service1)
            {
                _windowTitle = $"Договоры клиента: {row[0]}";
            }
        }
    }
}

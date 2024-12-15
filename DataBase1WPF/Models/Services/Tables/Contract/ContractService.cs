using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.DataBase.Repositories;
using DataBase1WPF.Models.Services.Tables.Order;
using DataBase1WPF.Models.Services.Tables.Payment;
using DataBase1WPF.Models.Services.Tables.WorkRecordCard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Contract
{
    public class ContractService : ITableService
    {
        private DataRow _selectedContract;
        private uint? _individualId;
        private uint? _juridicalPersonId;

        private OrderService _orderService;
        private PaymentService _paymentService;

        private Dictionary<DataRow, IContractDB> _dataDictionary;
        

        public ContractService() 
        {
            _orderService = new();
            _paymentService = new();
        }


        public DataTable GetValuesTable()
        {
            DataTable table = new();
            
            if (DataManager.GetInstance().ContractDB_Repository is ContractDB_Repository repository)
            {
                List<IContractDB> values = new();

                 if  ( _individualId != null )
                      values = repository.GetContractsByIndividualId((uint)_individualId).ToList();
                 else if ( _juridicalPersonId != null )
                    values = repository.GetContractsByJuridicalPersonId((uint)_juridicalPersonId).ToList();


                table = DataTableConverter.ToDataTable(values);

                for (int i = 0; i < 8; i++)
                {
                    table.Columns.Remove(table.Columns[0]);
                }
                table.Columns.Remove(table.Columns[3]);


                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }
            return table;
        }


        public DataTable GetValuesTableByIndividualId(uint individualId) 
        {
            _individualId = individualId;
            DataTable table = new();
            if (DataManager.GetInstance().ContractDB_Repository is ContractDB_Repository repository)
            {
                List<IContractDB> values = repository.GetContractsByIndividualId(individualId).ToList();

                table = DataTableConverter.ToDataTable(values);

                for (int i = 0; i < 8; i++)
                {
                    table.Columns.Remove(table.Columns[0]);
                }
                table.Columns.Remove(table.Columns[3]);


                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }
            return table;
        }


        public DataTable GetValuesTableByJuridicalPersonId(uint juridicalPersonId)
        {
            _juridicalPersonId = juridicalPersonId;
            DataTable table = new();
            if (DataManager.GetInstance().ContractDB_Repository is ContractDB_Repository repository)
            {
                List<IContractDB> values = repository.GetContractsByJuridicalPersonId(juridicalPersonId).ToList();

                table = DataTableConverter.ToDataTable(values);

                for (int i = 0; i < 8; i++)
                {
                    table.Columns.Remove(table.Columns[0]);
                }
                table.Columns.Remove(table.Columns[3]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }
            return table;
        }



        public string GetTableName()
        {
            return "Договоры";
        }

        public string GetOrdersTableName()
        {
            return "Заказы";
        }

        public string GetPaymentsTableName()
        {
            return "Платежи";
        }

        public DataTable SearchDataInTable(string searchLine)
        {
            DataTable table = new();

            if (DataManager.GetInstance().ContractDB_Repository is ContractDB_Repository repository)
            {
                List<IContractDB> values = new();

                if (_individualId != null)
                    values = repository.GetContractsByIndividualId((uint)_individualId).ToList();
                else if (_juridicalPersonId != null)
                    values = repository.GetContractsByJuridicalPersonId((uint)_juridicalPersonId).ToList();

                values = values.Where(item =>
                       item.EmployeeSurname.Contains(searchLine)
                    || item.EmployeeName.Contains(searchLine)
                    || item.EmployeePatronymic.Contains(searchLine)
                    || item.EmployeeSurname.Contains(searchLine)
                    || item.PaymentFrequencyTitle.Contains(searchLine)
                    || item.EmployeeSurname.Contains(searchLine)
                    || item.RegistrationNumber.Contains(searchLine)
                    || item.BeginOfAction.Contains(searchLine)
                    || item.EndOfAction.Contains(searchLine)
                    || item.AdditionalConditions.Contains(searchLine)
                    || item.Fine.ToString().Contains(searchLine)
                ).ToList();


                table = DataTableConverter.ToDataTable(values);

                for (int i = 0; i < 8; i++)
                {
                    table.Columns.Remove(table.Columns[0]);
                }
                table.Columns.Remove(table.Columns[3]);


                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }
            return table;
        }

        public List<string> GetDistricts()
        {
            List<string> districts = new();

            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            foreach (IHandbookDB districtDB in districtsDB)
                districts.Add(districtDB.Title);


            return districts;
        }

        public List<string> GetStreets()
        {
            List<string> streets = new();

            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            foreach (IHandbookDB streetDB in streetsDB)
                streets.Add(streetDB.Title);


            return streets;
        }

        public void Add(int employeeSelectedIndex, int paymentFrequencySelectedIndex,
            string registrationNumber,
            DateTime beginOfAction, DateTime endOfAction, string additionalConditions,
            float fine)
        {
            DataManager.GetInstance().ContractDB_Repository.Create(new ContractDB(
                _individualId,
                _juridicalPersonId,
                DataManager.GetInstance().EmployeeDB_Repository.Read().ToList()[employeeSelectedIndex].Id,
                DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList()[paymentFrequencySelectedIndex].Id,
                registrationNumber,
                beginOfAction.ToString("yyyy-MM-dd"),
                endOfAction.ToString("yyyy-MM-dd"),
                additionalConditions,
                fine
                ));
        }


        
        //public int GetPositionsSelectedIndex(DataRow row)
        //{
        //    return _workRecordCardService.GetPositionsSelectedIndex(row);
        //}


        public void Update(DataRow row, int employeeSelectedIndex, 
            int paymentFrequencySelectedIndex,
            string registrationNumber,
            DateTime beginOfAction, DateTime endOfAction, string additionalConditions,
            float fine)
        {
            DataManager.GetInstance().ContractDB_Repository.Update(new ContractDB(
                _dataDictionary[row].Id,
                _individualId,
                _juridicalPersonId,
                DataManager.GetInstance().EmployeeDB_Repository.Read().ToList()[employeeSelectedIndex].Id,
                DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList()[paymentFrequencySelectedIndex].Id,
                registrationNumber,
                beginOfAction.ToString("yyyy-MM-dd"),
                endOfAction.ToString("yyyy-MM-dd"),
                additionalConditions,
                fine
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().ContractDB_Repository.Delete(_dataDictionary[row].Id);
        }


        public DataTable? GetOrdersByContract(DataRow row)
        {
            _selectedContract = row;
            return _orderService.GetOrdersByContractId(_dataDictionary[row].Id);
        }

        public DataTable? GetPaymentsByContract(DataRow row)
        {
            _selectedContract = row;
            return _paymentService.GetPaymentsByContractId(_dataDictionary[row].Id);
        }


        public DataTable SearchDataInTableOrders(string searchLine)
        {
            return _orderService.SearchDataInTable(_dataDictionary[_selectedContract].Id, searchLine);
        }

        public DataTable SearchDataInTablePayments(string searchLine)
        {
            return _paymentService.SearchDataInTable(_dataDictionary[_selectedContract].Id, searchLine);
        }


        //public List<string> GetPositions()
        //{
        //    return _workRecordCardService.GetPositions();
        //}



        public void AddOrder(int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            _orderService.Add(_dataDictionary[_selectedContract].Id, premiseSelectedIndex,
                rentalPurposeSelectedIndex, beginOfRent, endOfRent, rentalPayment);
        }

        public void UpdateOrder(DataRow row, int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            _orderService.Update(row, _dataDictionary[_selectedContract].Id,
                premiseSelectedIndex, rentalPurposeSelectedIndex, beginOfRent, 
                endOfRent, rentalPayment);
        }

        public void DeleteOrder(DataRow row)
        {
            _orderService.Delete(row);
        }



        public void AddPayment(DateTime dateOfPayment,
            float amountOfPayment)
        {
            _paymentService.Add(_dataDictionary[_selectedContract].Id, dateOfPayment, 
                amountOfPayment);
        }

        public void UpdatePayment(DataRow row, DateTime dateOfPayment,
            float amountOfPayment)
        {
            _paymentService.Update(row, _dataDictionary[_selectedContract].Id, dateOfPayment,
                amountOfPayment);
        }

        public void DeletePayment(DataRow row)
        {
            _paymentService.Delete(row);
        }



        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            throw new NotImplementedException();
        }
    }
}

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
        private IWorkRecordCardService _workRecordCardService;
        private DataRow _selectedContract;
        private uint? _individualId;
        private uint? _juridicalPersonId;

        private OrderService _orderService;
        private PaymentService _paymentService;

        private Dictionary<DataRow, IContractDB> _dataDictionary;
        

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
            return new DataTable();
            //List<IEmployeeDB> values = GetValues().Where(item => item.DistrictTitle.Contains(searchLine)
            //|| item.StreetTitle.Contains(searchLine) || item.HouseNumber.Contains(searchLine)
            //|| item.Surname.Contains(searchLine) || item.Name.Contains(searchLine)
            //|| item.Patronymic.Contains(searchLine) || item.DateOfBirth.ToString().Contains(searchLine)).ToList();
            //DataTable table = DataTableConverter.ToDataTable(values);
            //table.Columns.Remove(table.Columns[0]);
            //table.Columns.Remove(table.Columns[5]);
            //table.Columns.Remove(table.Columns[6]);



            //_dataDictionary = new();
            //for (int i = 0; i < values.Count; i++)
            //    _dataDictionary.Add(table.Rows[i], values[i]);


            //return table;
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

        public void Add(int districtSelectedIndex, int streetSelectedIndex,
            string surname, string name, string? patronymic, DateTime dateOfBirth,
            string houseNumber)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Create(new EmployeeDB(
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                surname,
                name,
                patronymic,
                dateOfBirth.ToString("yyyy-MM-dd"),
                houseNumber
                ));
        }


        
        public int GetPositionsSelectedIndex(DataRow row)
        {
            return _workRecordCardService.GetPositionsSelectedIndex(row);
        }


        public void Update(DataRow row, int districtSelectedIndex, int streetSelectedIndex,
            string surname, string name, string? patronymic, DateTime dateOfBirth,
            string houseNumber)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Update(new EmployeeDB(
                _dataDictionary[row].Id,
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                surname,
                name,
                patronymic,
                dateOfBirth.ToString("yyyy-MM-dd"),
                houseNumber
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


        public DataTable SearchDataInTableWorkRecordCard(string searchLine)
        {
            return _workRecordCardService.SearchDataInTable(_dataDictionary[_selectedEmployee].Id, searchLine);
        }


        public List<string> GetPositions()
        {
            return _workRecordCardService.GetPositions();
        }



        public void AddOrder(int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Add(_dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        public void UpdateOrder(DataRow row, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Update(row, _dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        public void DeleteOrder(DataRow row)
        {
            _orderService.Delete(row);
        }



        public void AddPayment(int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Add(_dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        public void UpdatePayment(DataRow row, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Update(row, _dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
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

using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Fine;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using DataBase1WPF.DataBase.Repositories;
using DataBase1WPF.Models.Services.Tables.Order;
using DataBase1WPF.Models.Services.Tables.Payment;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Contract
{
    /// <summary>
    /// Сервис для договоров
    /// </summary>
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


        /// <summary>
        /// Получение значений договоров в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            DataTable table = new();
            
            if (DataManager.GetInstance().ContractDB_Repository is ContractDB_Repository repository)
            {
                List<IContractDB> values = new();

                if (_individualId != null)
                    values = repository.GetContractsByIndividualId((uint)_individualId).ToList();
                else if (_juridicalPersonId != null)
                    values = repository.GetContractsByJuridicalPersonId((uint)_juridicalPersonId).ToList();
                else
                    values = repository.Read().ToList();

                table = DataTableConverter.ToDataTable(values);

                
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[3]);
                table.Columns.Remove(table.Columns[4]);
                table.Columns.Remove(table.Columns[7]);


                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }
            return table;
        }


        /// <summary>
        /// Получение значений договоров по идентификатору физического лица в таблице DataTable
        /// </summary>
        /// <param name="individualId"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получение значений договоров по идентификатору юридического лица в таблице DataTable
        /// </summary>
        /// <param name="juridicalPersonId"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получение имени таблицы договоры
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Договоры";
        }

        /// <summary>
        /// Получение имени таблицы заказы
        /// </summary>
        /// <returns></returns>
        public string GetOrdersTableName()
        {
            return "Заказы";
        }


        /// <summary>
        /// Получение имени таблицы платежи
        /// </summary>
        /// <returns></returns>
        public string GetPaymentsTableName()
        {
            return "Платежи";
        }


        /// <summary>
        /// Поиск данных по таблице договоры
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Получить список периодичностей оплаты
        /// </summary>
        /// <returns></returns>
        public List<string> GetPaymentFrequencies()
        {
            List<string> paymentFrequencies = new();

            List<IHandbookDB> paymentFrequenciesDB = DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList();

            foreach (IHandbookDB paymentFrequency in paymentFrequenciesDB)
                paymentFrequencies.Add(paymentFrequency.Title);


            return paymentFrequencies;
        }


        /// <summary>
        /// Получение индекса периодичности оплаты у выбранного значения договора
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetPaymentFrequencySelectedIndex(DataRow row) 
        {
            List<IHandbookDB> paymentFrequenciesDB = DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList();

            return paymentFrequenciesDB.FindIndex((elem) => elem.Id == _dataDictionary[row].PaymentFrequencyId);
        }


        /// <summary>
        /// Получение сотрудников-менеджеров ( значения из базы данных )
        /// </summary>
        /// <returns></returns>
        public List<IEmployeeDB> GetEmployeeDB() 
        {
            List<IEmployeeDB> foundEmployees = new();
            List<IEmployeeDB> employeesDB = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();
            List<IPositionDB> positionsDB = DataManager.GetInstance().PositionDB_Repository.Read().ToList();


            foreach (IEmployeeDB employeeDB in employeesDB)
            {
                if (DataManager.GetInstance().WorkRecordCardDB_Repository is WorkRecordCardDB_Repository repository)
                {
                    List<IWorkRecordCardDB> workRecordCardsDB = repository.GetWorkRecordCardByEmployeeId(employeeDB.Id).ToList();
                    foreach (IWorkRecordCardDB workRecordCardDB in workRecordCardsDB)
                    {
                        foreach (IPositionDB positionDB in positionsDB)
                        {
                            if (workRecordCardDB.PositionId == positionDB.Id
                                && positionDB.Name == "Менеджер")
                                foundEmployees.Add(employeeDB);
                        }
                    }
                }

            }
            return foundEmployees;
        }


        /// <summary>
        /// Получение индекса сотрудника у выбранного значения договора
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetEmployeeSelectedIndex(DataRow row)
        {
            return GetEmployeeDB().FindIndex((elem) => elem.Id == _dataDictionary[row].EmployeeId);
        }

        /// <summary>
        /// Получение индекса штрафа у выбранного значения договора
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetFineSelectedIndex(DataRow row)
        {
            List<IFineDB> finesDB = DataManager.GetInstance().FineDB_Repository.Read().ToList();

            return finesDB.FindIndex((elem) => elem.Amount == _dataDictionary[row].Fine);
        }


        /// <summary>
        /// Получение списка сотрудников-менеджеров 
        /// </summary>
        /// <returns></returns>
        public List<string> GetEmployees()
        {
            List<string> employees = new();

            List<IEmployeeDB> employeesDB = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();
            List<IPositionDB> positionsDB = DataManager.GetInstance().PositionDB_Repository.Read().ToList();
            

            foreach (IEmployeeDB employeeDB in employeesDB)
            {
                if (DataManager.GetInstance().WorkRecordCardDB_Repository is WorkRecordCardDB_Repository repository)
                {
                    List<IWorkRecordCardDB> workRecordCardsDB = repository.GetWorkRecordCardByEmployeeId(employeeDB.Id).ToList();
                    foreach (IWorkRecordCardDB workRecordCardDB in workRecordCardsDB)
                    {
                        foreach (IPositionDB positionDB in positionsDB)
                        {
                            if (workRecordCardDB.PositionId == positionDB.Id
                                && positionDB.Name == "Менеджер")
                                employees.Add(employeeDB.Surname + " " + employeeDB.Name + " " + employeeDB.Patronymic);
                        }
                    }
                }
                
            }

            return employees;
        }

        /// <summary>
        /// Получение списка штрафов
        /// </summary>
        /// <returns></returns>
        public List<string> GetFines()
        {
            List<string> fines = new();

            List<IFineDB> finesDB = DataManager.GetInstance().FineDB_Repository.Read().ToList();

            foreach (IFineDB fineDB in finesDB)
                fines.Add(fineDB.Amount.ToString());


            return fines;
        }

        
        
        /// <summary>
        /// Добавление договора
        /// </summary>
        /// <param name="employeeSelectedIndex"></param>
        /// <param name="paymentFrequencySelectedIndex"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="beginOfAction"></param>
        /// <param name="endOfAction"></param>
        /// <param name="additionalConditions"></param>
        /// <param name="fineSelectedIndex"></param>
        public void Add(int employeeSelectedIndex, int paymentFrequencySelectedIndex,
            string registrationNumber,
            DateTime beginOfAction, DateTime endOfAction, string additionalConditions,
            int fineSelectedIndex)
        {
            DataManager.GetInstance().ContractDB_Repository.Create(new ContractDB(
                _individualId,
                _juridicalPersonId,
                GetEmployeeDB()[employeeSelectedIndex].Id,
                DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList()[paymentFrequencySelectedIndex].Id,
                registrationNumber,
                beginOfAction.ToString("yyyy-MM-dd"),
                endOfAction.ToString("yyyy-MM-dd"),
                additionalConditions,
                DataManager.GetInstance().FineDB_Repository.Read().ToList()[fineSelectedIndex].Amount
                ));
        }

        /// <summary>
        /// Изменение договора
        /// </summary>
        /// <param name="row"></param>
        /// <param name="employeeSelectedIndex"></param>
        /// <param name="paymentFrequencySelectedIndex"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="beginOfAction"></param>
        /// <param name="endOfAction"></param>
        /// <param name="additionalConditions"></param>
        /// <param name="fineSelectedIndex"></param>
        public void Update(DataRow row, int employeeSelectedIndex, 
            int paymentFrequencySelectedIndex,
            string registrationNumber,
            DateTime beginOfAction, DateTime endOfAction, string additionalConditions,
            int fineSelectedIndex)
        {
            DataManager.GetInstance().ContractDB_Repository.Update(new ContractDB(
                _dataDictionary[row].Id,
                _individualId,
                _juridicalPersonId,
                GetEmployeeDB()[employeeSelectedIndex].Id,
                DataManager.GetInstance().PaymentFrequencyDB_Repository.Read().ToList()[paymentFrequencySelectedIndex].Id,
                registrationNumber,
                beginOfAction.ToString("yyyy-MM-dd"),
                endOfAction.ToString("yyyy-MM-dd"),
                additionalConditions,
                DataManager.GetInstance().FineDB_Repository.Read().ToList()[fineSelectedIndex].Amount
                ));
        }


        /// <summary>
        /// Удаление договора
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().ContractDB_Repository.Delete(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение заказов в таблице DataTable по выбранному договору
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataTable? GetOrdersByContract(DataRow row)
        {
            _selectedContract = row;
            return _orderService.GetOrdersByContractId(_dataDictionary[row].Id);
        }

        /// <summary>
        /// Получение заказов (значения из базы данных) по выбранному договору
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<IOrderDB> GetOrdersDBbyContract(DataRow row)
        {
            _selectedContract = row;
            return _orderService.GetOrdersDBbyContractId(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение платежей в таблице DataTable по выбранному договору
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataTable? GetPaymentsByContract(DataRow row)
        {
            _selectedContract = row;
            return _paymentService.GetPaymentsByContractId(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Поиск данных по таблице заказы
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTableOrders(string searchLine)
        {
            return _orderService.SearchDataInTable(_dataDictionary[_selectedContract].Id, searchLine);
        }


        /// <summary>
        /// Поиск данных по таблице платежи
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTablePayments(string searchLine)
        {
            return _paymentService.SearchDataInTable(_dataDictionary[_selectedContract].Id, searchLine);
        }

        /// <summary>
        /// Получение списка помещений
        /// </summary>
        /// <returns></returns>
        public List<string> GetOrderPremises()
        {
            return _orderService.GetPremises();
        }

        /// <summary>
        /// Получение списка помещений для изменения данных
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<string> GetOrderPremisesForEdit(DataRow row)
        {
            return _orderService.GetPremisesForEdit(row);
        }

        /// <summary>
        /// Получение целей аренды
        /// </summary>
        /// <returns></returns>
        public List<string> GetOrdersRentalPurposes()
        {
            return _orderService.GetRentalPurposes();
        }

        /// <summary>
        /// Получение индекса цели аренды у выбранного значения заказа
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetOrdersRentalPurposesSelectedIndex(DataRow row)
        {
            return _orderService.GetRentalPurposesSelectedIndex(row);
        }

        /// <summary>
        /// Получение индекса помещения у выбранного значения заказа
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetOrdersPremisesSelectedIndex(DataRow row)
        {
            return _orderService.GetPremisesSelectedIndex(row);
        }


        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="premiseSelectedIndex"></param>
        /// <param name="rentalPurposeSelectedIndex"></param>
        /// <param name="beginOfRent"></param>
        /// <param name="endOfRent"></param>
        /// <param name="rentalPayment"></param>
        public void AddOrder(int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            _orderService.Add(_dataDictionary[_selectedContract].Id, premiseSelectedIndex,
                rentalPurposeSelectedIndex, beginOfRent, endOfRent, rentalPayment);
        }


        /// <summary>
        /// Изменение заказа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="premiseSelectedIndex"></param>
        /// <param name="rentalPurposeSelectedIndex"></param>
        /// <param name="beginOfRent"></param>
        /// <param name="endOfRent"></param>
        /// <param name="rentalPayment"></param>
        public void UpdateOrder(DataRow row, int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            _orderService.Update(row, _dataDictionary[_selectedContract].Id,
                premiseSelectedIndex, rentalPurposeSelectedIndex, beginOfRent, 
                endOfRent, rentalPayment);
        }


        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="row"></param>
        public void DeleteOrder(DataRow row)
        {
            _orderService.Delete(row);
        }


        /// <summary>
        /// Добавление платежа
        /// </summary>
        /// <param name="dateOfPayment"></param>
        /// <param name="amountOfPayment"></param>
        public void AddPayment(DateTime dateOfPayment,
            float amountOfPayment)
        {
            _paymentService.Add(_dataDictionary[_selectedContract].Id, dateOfPayment, 
                amountOfPayment);
        }

        /// <summary>
        /// Изменение платежа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dateOfPayment"></param>
        /// <param name="amountOfPayment"></param>
        public void UpdatePayment(DataRow row, DateTime dateOfPayment,
            float amountOfPayment)
        {
            _paymentService.Update(row, _dataDictionary[_selectedContract].Id, dateOfPayment,
                amountOfPayment);
        }

        /// <summary>
        /// Удаление платежа
        /// </summary>
        /// <param name="row"></param>
        public void DeletePayment(DataRow row)
        {
            _paymentService.Delete(row);
        }

        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Экспорт в MS Word
        /// </summary>
        /// <param name="row"></param>
        public void ExportWord(DataRow row)
        {
            IJuridicalPersonDB juridicalPersonDB = null;
            if (_dataDictionary[row].JuridicalPersonId != 0)
            {
                if (DataManager.GetInstance().JuridicalPersonDB_Repository is JuridicalPersonDB_Repository repository)
                {
                    juridicalPersonDB = repository.GetDirectorFullNameByContractId(_dataDictionary[row].Id);
                }
            }
            ExportToWord.ExportContract(_dataDictionary[row], GetOrdersByContract(row), juridicalPersonDB);
        }

        /// <summary>
        /// Экспорт в MS Excel
        /// </summary>
        /// <param name="table"></param>
        public void ExportExcel(DataTable table)
        {
            ExportToExcel.ExportTable(table);
        }

    }
}

using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.Models.Services.Tables.WorkRecordCard;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Employee
{
    /// <summary>
    /// Сервис для сотрудников
    /// </summary>
    public class EmployeeService : IEmployeeService, ITableService
    {
        private IWorkRecordCardService _workRecordCardService;
        private DataRow _selectedEmployee;

        private Dictionary<DataRow, IEmployeeDB> _dataDictionary;

        /// <summary>
        /// Получение данных сотрудников
        /// </summary>
        /// <returns></returns>
        private List<IEmployeeDB> GetValues()
        {
            List<IEmployeeDB> values = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();
            return values;
        }

        public EmployeeService()
        {
            _workRecordCardService = new WorkRecordCardService();
        }


        /// <summary>
        /// Получение данных сотрудников в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IEmployeeDB> values = GetValues();
            
            DataTable table = DataTableConverter.ToDataTable(values);
            
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[5]);
            table.Columns.Remove(table.Columns[6]);


            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }


        /// <summary>
        /// Получение имени таблицы сотрудники
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Сотрудники";
        }

        /// <summary>
        /// Получение имени таблицы трудовая книжка
        /// </summary>
        /// <returns></returns>
        public string GetOtherTableName()
        {
            return "Трудовая книжка";
        }

        /// <summary>
        /// Поиск данных по таблице сотрудники
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IEmployeeDB> values = GetValues().Where(item => item.DistrictTitle.Contains(searchLine)
            || item.StreetTitle.Contains(searchLine) || item.HouseNumber.Contains(searchLine) || item.FlatNumber.Contains(searchLine)
            || item.Surname.Contains(searchLine) || item.Name.Contains(searchLine)
            || item.Patronymic.Contains(searchLine) || item.DateOfBirth.ToString().Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[5]);
            table.Columns.Remove(table.Columns[6]);

            

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }

        /// <summary>
        /// Получение прав пользователя к сотрудникам
        /// </summary>
        /// <param name="menuElemId"></param>
        /// <returns></returns>
        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            UserAbilitiesType userAbilities = new();
            List<IUserAbilitiesDB> userAbilitiesDB = DataManager.GetInstance().UserAbilitiesDB_Repository.Read().ToList();


            foreach (IUserAbilitiesDB userAbilityDB in userAbilitiesDB)
            {
                if (userAbilityDB.UserId == DataManager.GetInstance().CurrentUser.Id
                    && userAbilityDB.MenuElemId == menuElemId)
                {
                    userAbilities.CanRead = userAbilityDB.R;
                    userAbilities.CanWrite = userAbilityDB.W;
                    userAbilities.CanEdit = userAbilityDB.E;
                    userAbilities.CanDelete = userAbilityDB.D;
                }
            }

            return userAbilities;
        }

        /// <summary>
        /// Получение списка районов
        /// </summary>
        /// <returns></returns>
        public List<string> GetDistricts()
        {
            List<string> districts = new();

            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            foreach (IHandbookDB districtDB in districtsDB)
                districts.Add(districtDB.Title);


            return districts;
        }


        /// <summary>
        /// Получение списка улиц
        /// </summary>
        /// <returns></returns>
        public List<string> GetStreets()
        {
            List<string> streets = new();

            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            foreach (IHandbookDB streetDB in streetsDB)
                streets.Add(streetDB.Title);


            return streets;
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="houseNumber"></param>
        /// <param name="flatNumber"></param>
        public void Add(int districtSelectedIndex, int streetSelectedIndex,
            string surname, string name, string? patronymic, DateTime dateOfBirth,
            string houseNumber, string flatNumber)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Create(new EmployeeDB(
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                surname,
                name,
                patronymic,
                dateOfBirth.ToString("yyyy-MM-dd"),
                houseNumber,
                flatNumber
                ));
        }

        /// <summary>
        /// Получение индекса района у выбранного сотрудника
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetDistrictSelectedIndex(DataRow row)
        {
            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            return districtsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].DistrictId);

        }


        /// <summary>
        /// Получение индекса улицы у выбранного сотрудника
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetStreetsSelectedIndex(DataRow row)
        {
            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            return streetsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].StreetId);

        }



        /// <summary>
        /// Получение индекса должности у выбранного сотрудника
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetPositionsSelectedIndex(DataRow row)
        {
            return _workRecordCardService.GetPositionsSelectedIndex(row);
        }

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="row"></param>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="houseNumber"></param>
        /// <param name="flatNumber"></param>
        public void Update(DataRow row, int districtSelectedIndex, int streetSelectedIndex,
            string surname, string name, string? patronymic, DateTime dateOfBirth,
            string houseNumber, string flatNumber)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Update(new EmployeeDB(
                _dataDictionary[row].Id,
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                surname,
                name,
                patronymic,
                dateOfBirth.ToString("yyyy-MM-dd"),
                houseNumber,
                flatNumber
                ));
        }


        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Delete(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение записи в трудовой книжке по сотруднику
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataTable? GetWorkRecordCardByEmployee(DataRow row)
        {
            _selectedEmployee = row;
            return _workRecordCardService.GetWorkRecordCardByEmployee(_dataDictionary[row].Id);
        }

        /// <summary>
        /// Получение частичных данных сотрудника
        /// </summary>
        /// <returns></returns>
        public string GetSelectedEmployeeText()
        {
            return _dataDictionary[_selectedEmployee].Surname + " "
                + _dataDictionary[_selectedEmployee].Name + " "
                + _dataDictionary[_selectedEmployee].Patronymic;
        }

        /// <summary>
        /// Поиск данных по таблице записей в трудовой книжке
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTableWorkRecordCard(string searchLine)
        {
            return _workRecordCardService.SearchDataInTable(_dataDictionary[_selectedEmployee].Id, searchLine);
        }

        /// <summary>
        /// Получение должностей
        /// </summary>
        /// <returns></returns>
        public List<string> GetPositions()
        {
            return _workRecordCardService.GetPositions();
        }


        /// <summary>
        /// Добавление записи в трудовую книжку
        /// </summary>
        /// <param name="positionIndex"></param>
        /// <param name="orderNumber"></param>
        /// <param name="orderDate"></param>
        /// <param name="reasonOfRecording"></param>
        public void AddWorkRecordCard(int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Add(_dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        /// <summary>
        /// Изменение записи в трудовую книжку
        /// </summary>
        /// <param name="row"></param>
        /// <param name="positionIndex"></param>
        /// <param name="orderNumber"></param>
        /// <param name="orderDate"></param>
        /// <param name="reasonOfRecording"></param>
        public void UpdateWorkRecordCard(DataRow row, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Update(row, _dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }


        /// <summary>
        /// Удаление записи в трудовой книжке
        /// </summary>
        /// <param name="row"></param>
        public void DeleteWorkRecordCard(DataRow row)
        {
            _workRecordCardService.Delete(row);
        }
    }
}

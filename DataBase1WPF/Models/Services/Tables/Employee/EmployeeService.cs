using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using DataBase1WPF.Models.Services.Tables.Premise;
using DataBase1WPF.Models.Services.Tables.WorkRecordCard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Employee
{
    public class EmployeeService : IEmployeeService, ITableService
    {
        private IWorkRecordCardService _workRecordCardService;
        private DataRow _selectedEmployee;

        private Dictionary<DataRow, IEmployeeDB> _dataDictionary;
        private List<IEmployeeDB> GetValues()
        {
            List<IEmployeeDB> values = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();
            return values;
        }

        public EmployeeService()
        {
            _workRecordCardService = new WorkRecordCardService();
        }


        public DataTable GetValuesTable()
        {
            List<IEmployeeDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[2]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }

        public string GetTableName()
        {
            return "Сотрудники";
        }

        public string GetOtherTableName()
        {
            return "Трудовая книжка";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IEmployeeDB> values = GetValues().Where(item => item.DistrictTitle.Contains(searchLine)
            || item.StreetTitle.Contains(searchLine) || item.HouseNumber.Contains(searchLine)
            || item.Surname.Contains(searchLine) || item.Name.Contains(searchLine)
            || item.Patronymic.Contains(searchLine) || item.DateOfBirth.ToString().Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[1]);
            table.Columns.Remove(table.Columns[2]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


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
                dateOfBirth,
                houseNumber
                ));
        }


        public int GetDistrictSelectedIndex(DataRow row)
        {
            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            return districtsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].DistrictId);

        }

        public int GetStreetsSelectedIndex(DataRow row)
        {
            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            return streetsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].StreetId);

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
                dateOfBirth,
                houseNumber
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().EmployeeDB_Repository.Delete(_dataDictionary[row].Id);
        }


        public DataTable? GetWorkRecordCardByEmployee(DataRow row)
        {
            _selectedEmployee = row;
            return _workRecordCardService.GetWorkRecordCardByEmployee(_dataDictionary[row].Id);
        }
        public string GetSelectedEmployeeText()
        {
            return _dataDictionary[_selectedEmployee].Surname + " "
                + _dataDictionary[_selectedEmployee].Name + " "
                + _dataDictionary[_selectedEmployee].Patronymic;
        }

        public DataTable SearchDataInTableWorkRecordCard(string searchLine)
        {
            return _workRecordCardService.SearchDataInTable(_dataDictionary[_selectedEmployee].Id, searchLine);
        }


        public List<string> GetPositions()
        {
            return _workRecordCardService.GetPositions();
        }



        public void AddWorkRecordCard(int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Add(_dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        public void UpdateWorkRecordCard(DataRow row, int positionIndex, string orderNumber,
            DateTime orderDate, string reasonOfRecording)
        {
            _workRecordCardService.Update(row, _dataDictionary[_selectedEmployee].Id, positionIndex,
                orderNumber, orderDate, reasonOfRecording);
        }

        public void DeleteWorkRecordCard(DataRow row)
        {
            _workRecordCardService.Delete(row);
        }
    }
}

using DataBase1WPF.DataBase.Entities.Individual;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.JuridicalPerson
{
    public class JuridicalPersonService : ITableService
    {
        private Dictionary<DataRow, IIndividualDB> _dataDictionary;
        private DataRow _selectedIndividual;
        private UserAbilitiesType _userAbilities;

        public UserAbilitiesType UserAbilities => _userAbilities;
        private List<IIndividualDB> GetValues()
        {
            List<IIndividualDB> values = DataManager.GetInstance().IndividualDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IIndividualDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }

        public string GetTableName()
        {
            return "Физические лица";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IIndividualDB> values = GetValues().Where(item => item.Surname.Contains(searchLine)
            || item.Name.Contains(searchLine) || item.Patronymic.Contains(searchLine)
            || item.PhoneNumber.Contains(searchLine) || item.PassportSeries.Contains(searchLine)
            || item.PassportNumber.Contains(searchLine) || item.DateOfIssue.Contains(searchLine)
            || item.IssuedBy.Contains(searchLine)).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        public UserAbilitiesType GetUserAbilities(uint menuElemId)
        {
            _userAbilities = new();
            List<IUserAbilitiesDB> userAbilitiesDB = DataManager.GetInstance().UserAbilitiesDB_Repository.Read().ToList();


            foreach (IUserAbilitiesDB userAbilityDB in userAbilitiesDB)
            {
                if (userAbilityDB.UserId == DataManager.GetInstance().CurrentUser.Id
                    && userAbilityDB.MenuElemId == menuElemId)
                {
                    _userAbilities.CanRead = userAbilityDB.R;
                    _userAbilities.CanWrite = userAbilityDB.W;
                    _userAbilities.CanEdit = userAbilityDB.E;
                    _userAbilities.CanDelete = userAbilityDB.D;
                }
            }

            return _userAbilities;
        }


        public void Add(string surname, string name, string? patronymic,
            string phoneNumber, string passportSeries,
            string passportNumber, DateTime dateOfIssue, string issuedBy)
        {
            DataManager.GetInstance().IndividualDB_Repository.Create(new IndividualDB(
                surname,
                name,
                patronymic,
                phoneNumber,
                passportSeries,
                passportNumber,
                dateOfIssue.ToString("yyyy-MM-dd"),
                issuedBy
                ));
        }

        public void Update(DataRow row, string surname, string name, string? patronymic,
            string phoneNumber, string passportSeries,
            string passportNumber, DateTime dateOfIssue, string issuedBy)
        {
            DataManager.GetInstance().IndividualDB_Repository.Update(new IndividualDB(
                _dataDictionary[row].Id,
                surname,
                name,
                patronymic,
                phoneNumber,
                passportSeries,
                passportNumber,
                dateOfIssue.ToString("yyyy-MM-dd"),
                issuedBy
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().IndividualDB_Repository.Delete(_dataDictionary[row].Id);
        }


        //public DataTable? GetContractByIndividuals(DataRow row)
        //{
        //    _selectedEmployee = row;
        //    return _workRecordCardService.GetWorkRecordCardByEmployee(_dataDictionary[row].Id);
        //}
        //public string GetSelectedEmployeeText()
        //{
        //    return _dataDictionary[_selectedEmployee].Surname + " "
        //        + _dataDictionary[_selectedEmployee].Name + " "
        //        + _dataDictionary[_selectedEmployee].Patronymic;
        //}
    }
}

using DataBase1WPF.DataBase.Entities.Individual;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Individual
{
    /// <summary>
    /// Сервис для физических лиц
    /// </summary>
    public class IndividualService : ITableService
    {
        private Dictionary<DataRow, IIndividualDB> _dataDictionary;
        private DataRow _selectedIndividual;
        private UserAbilitiesType _userAbilities;

        public UserAbilitiesType UserAbilities => _userAbilities;


        /// <summary>
        /// Получение данных физических лиц
        /// </summary>
        /// <returns></returns>
        private List<IIndividualDB> GetValues()
        {
            List<IIndividualDB> values = DataManager.GetInstance().IndividualDB_Repository.Read().ToList();
            return values;
        }


        /// <summary>
        /// Получение данных физических лиц в таблице DataTable
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Получение имени таблицы физические лица
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Физические лица";
        }


        /// <summary>
        /// Поиск данных по таблице физические лица
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение прав пользователя к физическим лицам
        /// </summary>
        /// <param name="menuElemId"></param>
        /// <returns></returns>
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

         

        /// <summary>
        /// Добавление физического лица
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportNumber"></param>
        /// <param name="dateOfIssue"></param>
        /// <param name="issuedBy"></param>
        public void Add(string surname, string name, string? patronymic,
            string phoneNumber, string passportSeries,
            string passportNumber, DateTime  dateOfIssue, string issuedBy)
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




        /// <summary>
        /// Изменение физического лица
        /// </summary>
        /// <param name="row"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportNumber"></param>
        /// <param name="dateOfIssue"></param>
        /// <param name="issuedBy"></param>
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



        /// <summary>
        /// Удаление физического лица
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().IndividualDB_Repository.Delete(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение идентификатора выбранного физического лица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public uint GetIndividualId(DataRow row)
        {
            return _dataDictionary[row].Id;
        }
        
    }
}

using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.JuridicalPerson
{
    /// <summary>
    /// Сервис для юридических лиц
    /// </summary>
    public class JuridicalPersonService : ITableService
    {
        private Dictionary<DataRow, IJuridicalPersonDB> _dataDictionary;
        private DataRow _selectedIndividual;
        private UserAbilitiesType _userAbilities;

        public UserAbilitiesType UserAbilities => _userAbilities;

        /// <summary>
        /// Получение данных юридических лиц
        /// </summary>
        /// <returns></returns>
        private List<IJuridicalPersonDB> GetValues()
        {
            List<IJuridicalPersonDB> values = DataManager.GetInstance().JuridicalPersonDB_Repository.Read().ToList();
            return values;
        }


        /// <summary>
        /// Получение данных юридических лиц в таблице DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetValuesTable()
        {
            List<IJuridicalPersonDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[4]);
            table.Columns.Remove(table.Columns[5]);
            table.Columns.Remove(table.Columns[9]);


            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }


        /// <summary>
        /// Получение имени таблицы юридические лица
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return "Юридические лица";
        }

        /// <summary>
        /// Поиск данных по таблице юридические лица
        /// </summary>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IJuridicalPersonDB> values = GetValues().Where(item => 
               item.OrganizationDistrictTitle.Contains(searchLine)
            || item.OrganizationStreetTitle.Contains(searchLine)
            || item.BankTitle.Contains(searchLine)
            || item.NameOfOrganization.Contains(searchLine)
            || item.DirectorSurname.Contains(searchLine)
            || item.DirectorName.Contains(searchLine)
            || item.DirectorPatronymic.Contains(searchLine)
            || item.OrganizationHouseNumber.Contains(searchLine)
            || item.PhoneNumber.Contains(searchLine)
            || item.PaymentAccount.Contains(searchLine)
            || item.IndividualTaxpayerNumber.Contains(searchLine)
               ).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);
            table.Columns.Remove(table.Columns[4]);
            table.Columns.Remove(table.Columns[5]);
            table.Columns.Remove(table.Columns[9]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);


            return table;
        }


        /// <summary>
        /// Получение прав пользователя к юрдическим лицам
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
        /// Получение списка банков
        /// </summary>
        /// <returns></returns>
        public List<string> GetBanks()
        {
            List<string> banks = new();

            List<IHandbookDB> banksDB = DataManager.GetInstance().BankDB_Repository.Read().ToList();

            foreach (IHandbookDB bankDB in banksDB)
                banks.Add(bankDB.Title);


            return banks;
        }



        /// <summary>
        /// Получение индекса района у выбранного  юридического лица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetDistrictSelectedIndex(DataRow row)
        {
            List<IHandbookDB> districtsDB = DataManager.GetInstance().DistrictDB_Repository.Read().ToList();

            return districtsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].OrganizationDistrictId);

        }


        /// <summary>
        /// Получение индекса улицы у выбранного  юридического лица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetStreetsSelectedIndex(DataRow row)
        {
            List<IHandbookDB> streetsDB = DataManager.GetInstance().StreetDB_Repository.Read().ToList();

            return streetsDB.FindIndex((elem) => elem.Id == _dataDictionary[row].OrganizationStreetId);

        }


        /// <summary>
        /// Получение индекса банка у выбранного  юридического лица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetBanksSelectedIndex(DataRow row)
        {
            List<IHandbookDB> banksDB = DataManager.GetInstance().BankDB_Repository.Read().ToList();

            return banksDB.FindIndex((elem) => elem.Id == _dataDictionary[row].BankId);

        }


        /// <summary>
        /// Добавление юридического лица
        /// </summary>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="bankSelectedIndex"></param>
        /// <param name="nameOfOrganization"></param>
        /// <param name="directorSurname"></param>
        /// <param name="directorName"></param>
        /// <param name="directorPatronymic"></param>
        /// <param name="organizationHouseNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="paymentAccount"></param>
        /// <param name="individualTaxpayerNumber"></param>
        public void Add(int districtSelectedIndex,
            int streetSelectedIndex, int bankSelectedIndex, string nameOfOrganization,
            string directorSurname, string directorName, string directorPatronymic,
            string organizationHouseNumber, string phoneNumber, string paymentAccount,
            string individualTaxpayerNumber)
        {
            DataManager.GetInstance().JuridicalPersonDB_Repository.Create(new JuridicalPersonDB(
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                DataManager.GetInstance().BankDB_Repository.Read().ToList()[bankSelectedIndex].Id,
                nameOfOrganization,
                directorSurname,  
                directorName,  
                directorPatronymic,
                organizationHouseNumber,  
                phoneNumber,  
                paymentAccount,
                individualTaxpayerNumber
                ));
        }


        /// <summary>
        /// Изменение юридического лица
        /// </summary>
        /// <param name="row"></param>
        /// <param name="districtSelectedIndex"></param>
        /// <param name="streetSelectedIndex"></param>
        /// <param name="bankSelectedIndex"></param>
        /// <param name="nameOfOrganization"></param>
        /// <param name="directorSurname"></param>
        /// <param name="directorName"></param>
        /// <param name="directorPatronymic"></param>
        /// <param name="organizationHouseNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="paymentAccount"></param>
        /// <param name="individualTaxpayerNumber"></param>
        public void Update(DataRow row, int districtSelectedIndex,
            int streetSelectedIndex, int bankSelectedIndex, string nameOfOrganization,
            string directorSurname, string directorName, string directorPatronymic,
            string organizationHouseNumber, string phoneNumber, string paymentAccount,
            string individualTaxpayerNumber)
        {
            DataManager.GetInstance().JuridicalPersonDB_Repository.Update(new JuridicalPersonDB(
                _dataDictionary[row].Id,
                DataManager.GetInstance().DistrictDB_Repository.Read().ToList()[districtSelectedIndex].Id,
                DataManager.GetInstance().StreetDB_Repository.Read().ToList()[streetSelectedIndex].Id,
                DataManager.GetInstance().BankDB_Repository.Read().ToList()[bankSelectedIndex].Id,
                nameOfOrganization,
                directorSurname,
                directorName,
                directorPatronymic,
                organizationHouseNumber,
                phoneNumber,
                paymentAccount,
                individualTaxpayerNumber
                ));
        }



        /// <summary>
        /// Удаление юридического лица
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().JuridicalPersonDB_Repository.Delete(_dataDictionary[row].Id);
        }


        /// <summary>
        /// Получение идентификатора выбранного юридического лица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public uint GetJuridicalPersonId(DataRow row)
        {
            return _dataDictionary[row].Id;
        }
    }
}

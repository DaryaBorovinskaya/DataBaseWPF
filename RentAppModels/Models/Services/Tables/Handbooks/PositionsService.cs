using DataBase1WPF.DataBase.Entities.Position;
using DataBase1WPF.DataBase.Entities.UserAbilities;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Handbooks
{
    public class PositionsService :  ITableService
    {
        private Dictionary<DataRow, IPositionDB> _dataDictionary;
        private List<IPositionDB> GetValues()
        {
            List<IPositionDB> values = DataManager.GetInstance().PositionDB_Repository.Read().ToList();
            return values;
        }

        public DataTable GetValuesTable()
        {
            List<IPositionDB> values = GetValues();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

            _dataDictionary = new();
            for (int i = 0; i < values.Count; i++)
                _dataDictionary.Add(table.Rows[i], values[i]);

            return table;
        }

        public string GetTableName()
        {
            return "Должности";
        }
        public DataTable SearchDataInTable(string searchLine)
        {
            List<IPositionDB> values = GetValues().Where(
                item => (item.Name.Contains(searchLine) || item.Salary.ToString().Contains(searchLine))).ToList();
            DataTable table = DataTableConverter.ToDataTable(values);
            table.Columns.Remove(table.Columns[0]);

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

        public void Add(string title, float salary)
        {
            DataManager.GetInstance().PositionDB_Repository.Create(new PositionDB(title, salary));
        }

        public void Update(DataRow row, string title, float salary) 
        {
            DataManager.GetInstance().PositionDB_Repository.Update(new PositionDB(_dataDictionary[row].Id, title, salary));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PositionDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

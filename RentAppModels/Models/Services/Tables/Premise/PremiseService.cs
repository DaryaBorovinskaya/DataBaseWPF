using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Premise
{
    /// <summary>
    /// Сервис для помещений
    /// </summary>
    public class PremiseService : IPremiseService
    {
        private Dictionary<DataRow, IPremiseDB> _dataDictionary;

        /// <summary>
        /// Получение помещений в таблице DataTable по идентификатору здания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable? GetPremisesByBuilding(uint id)
        {
            DataTable table = null;
            if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
            {
                List<IPremiseDB> values = repository.GetPremisesByBuildingId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }


        /// <summary>
        /// Поиск данных по таблице помещений
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public DataTable SearchDataInTable(uint buildingId, string searchLine)
        {
            DataTable table = new ();
            if (DataManager.GetInstance().PremiseDB_Repository is PremiseDB_Repository repository)
            {
                List<IPremiseDB> values = repository.GetPremisesByBuildingId(buildingId).ToList().Where(
                    item => item.TypeOfFinishingTitle.Contains(searchLine)
                    || item.PremiseNumber.Contains(searchLine) || item.Area.ToString().Contains(searchLine)
                    || item.FloorNumber.ToString().Contains(searchLine) 
                    || item.TempRentalPayment.ToString().Contains(searchLine)).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);

            }
            return table;
        }

        /// <summary>
        /// Получение списка видов отделки
        /// </summary>
        /// <returns></returns>
        public List<string> GetTypesOfFinishing()
        {
            List<string> typesOfFinishing = new();

            List<IHandbookDB> typesOfFinishingDB = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

            foreach (IHandbookDB typeOfFinishingDB in typesOfFinishingDB)
                typesOfFinishing.Add(typeOfFinishingDB.Title);


            return typesOfFinishing;
        }


        /// <summary>
        /// Получение индекса вида отделки у выбранного  помещения
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetTypeOfFinishingSelectedIndex(DataRow row)
        {
            List<IHandbookDB> typeOfFinishing = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

            return typeOfFinishing.FindIndex((elem) => elem.Id == _dataDictionary[row].TypeOfFinishingId);
        }


        /// <summary>
        /// Добавление помещения
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="typeOfFinishingIndex"></param>
        /// <param name="premiseNumber"></param>
        /// <param name="area"></param>
        /// <param name="floorNumber"></param>
        /// <param name="availAbilityOfPhoneNumber"></param>
        /// <param name="tempRentalPayment"></param>
        public void Add(uint buildingId, int typeOfFinishingIndex, string premiseNumber,
            float area, int floorNumber, bool availAbilityOfPhoneNumber, 
            float tempRentalPayment )
        {
            DataManager.GetInstance().PremiseDB_Repository.Create(new PremiseDB(
                buildingId,
                DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList()[typeOfFinishingIndex].Id,
                premiseNumber,
                area,
                floorNumber,
                availAbilityOfPhoneNumber,
                tempRentalPayment
                ));
        }

        /// <summary>
        /// Изменение помещения
        /// </summary>
        /// <param name="row"></param>
        /// <param name="buildingId"></param>
        /// <param name="typeOfFinishingIndex"></param>
        /// <param name="premiseNumber"></param>
        /// <param name="area"></param>
        /// <param name="floorNumber"></param>
        /// <param name="availAbilityOfPhoneNumber"></param>
        /// <param name="tempRentalPayment"></param>
        public void Update(DataRow row, uint buildingId, int typeOfFinishingIndex, string premiseNumber, float area, int floorNumber, bool availAbilityOfPhoneNumber, float tempRentalPayment)
        {
            DataManager.GetInstance().PremiseDB_Repository.Update(new PremiseDB(
                _dataDictionary[row].Id,
                buildingId,
                DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList()[typeOfFinishingIndex].Id,
                premiseNumber,
                area,
                floorNumber,
                availAbilityOfPhoneNumber,
                tempRentalPayment
                ));
        }

        /// <summary>
        /// Удаление помещения
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PremiseDB_Repository.Delete(_dataDictionary[row].Id);
        }

        
    }
}

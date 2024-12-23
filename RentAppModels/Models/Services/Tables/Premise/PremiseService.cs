using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Premise
{
    public class PremiseService : IPremiseService
    {
        private Dictionary<DataRow, IPremiseDB> _dataDictionary;
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


        public List<string> GetTypesOfFinishing()
        {
            List<string> typesOfFinishing = new();

            List<IHandbookDB> typesOfFinishingDB = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

            foreach (IHandbookDB typeOfFinishingDB in typesOfFinishingDB)
                typesOfFinishing.Add(typeOfFinishingDB.Title);


            return typesOfFinishing;
        }

        public int GetTypeOfFinishingSelectedIndex(DataRow row)
        {
            List<IHandbookDB> typeOfFinishing = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

            return typeOfFinishing.FindIndex((elem) => elem.Id == _dataDictionary[row].TypeOfFinishingId);
        }



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

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PremiseDB_Repository.Delete(_dataDictionary[row].Id);
        }

        
    }
}

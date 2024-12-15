using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Order
{
    public class OrderService 
    {
        private Dictionary<DataRow, IOrderDB> _dataDictionary;
        public DataTable? GetOrdersByContractId(uint id)
        {
            DataTable table = null;
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                List<IOrderDB> values = repository.GetOrdersByContractId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[1]);
                table.Columns.Remove(table.Columns[2]);
                table.Columns.Remove(table.Columns[6]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }

        public DataTable SearchDataInTable(uint buildingId, string searchLine)
        {
            DataTable table = new();
            if (DataManager.GetInstance().OrderDB_Repository is OrderDB_Repository repository)
            {
                List<IOrderDB> values = repository.GetOrdersByContractId(buildingId).ToList().Where(
                    item => 
                       item.DistrictTitle.Contains(searchLine)
                    || item.StreetTitle.Contains(searchLine)
                    || item.HouseNumber.Contains(searchLine)
                    || item.FloorNumber.ToString().Contains(searchLine)
                    || item.PremiseNumber.Contains(searchLine)
                    || item.Area.ToString().Contains(searchLine)
                    || item.RentalPurposeTitle.Contains(searchLine)
                    || item.BeginOfRent.Contains(searchLine)
                    || item.EndOfRent.Contains(searchLine)
                    || item.RentalPayment.ToString().Contains(searchLine)).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[1]);
                table.Columns.Remove(table.Columns[2]);
                table.Columns.Remove(table.Columns[6]);

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

        //public int GetTypeOfFinishingSelectedIndex(DataRow row)
        //{
        //    List<IHandbookDB> typeOfFinishing = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

        //    return typeOfFinishing.FindIndex((elem) => elem.Id == _dataDictionary[row].TypeOfFinishingId);
        //}



        public void Add(uint contractId, int premiseSelectedIndex, 
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            DataManager.GetInstance().OrderDB_Repository.Create(new OrderDB(
                contractId,
                DataManager.GetInstance().PremiseDB_Repository.Read().ToList()[premiseSelectedIndex].Id,
                DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList()[rentalPurposeSelectedIndex].Id,
                beginOfRent.ToString("yyyy-MM-dd"),
                endOfRent.ToString("yyyy-MM-dd"),
                rentalPayment
                ));
        }

        public void Update(DataRow row, uint contractId, int premiseSelectedIndex,
            int rentalPurposeSelectedIndex,
            DateTime beginOfRent, DateTime endOfRent,
            float rentalPayment)
        {
            DataManager.GetInstance().OrderDB_Repository.Update(new OrderDB(
                _dataDictionary[row].Id,
                contractId,
                DataManager.GetInstance().PremiseDB_Repository.Read().ToList()[premiseSelectedIndex].Id,
                DataManager.GetInstance().RentalPurposeDB_Repository.Read().ToList()[rentalPurposeSelectedIndex].Id,
                beginOfRent.ToString("yyyy-MM-dd"),
                endOfRent.ToString("yyyy-MM-dd"),
                rentalPayment
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().OrderDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

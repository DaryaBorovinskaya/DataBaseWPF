using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Payment;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Payment
{
    public class PaymentService
    {
        private Dictionary<DataRow, IPaymentDB> _dataDictionary;
        public DataTable? GetPaymentsByContractId(uint id)
        {
            DataTable table = null;
            if (DataManager.GetInstance().PaymentDB_Repository is PaymentDB_Repository repository)
            {
                List<IPaymentDB> values = repository.GetPaymentsByContractId(id).ToList();
                table = DataTableConverter.ToDataTable(values);
                table.Columns.Remove(table.Columns[0]);
                table.Columns.Remove(table.Columns[0]);

                _dataDictionary = new();
                for (int i = 0; i < values.Count; i++)
                    _dataDictionary.Add(table.Rows[i], values[i]);
            }



            return table;
        }

        public DataTable SearchDataInTable(uint contractId, string searchLine)
        {
            DataTable table = new();
            if (DataManager.GetInstance().PaymentDB_Repository is PaymentDB_Repository repository)
            {
                List<IPaymentDB> values = repository.GetPaymentsByContractId(contractId).ToList().Where(
                    item => 
                    item.DateOfPayment.Contains(searchLine)
                    || item.AmountOfPayment.ToString().Contains(searchLine)).ToList();
                table = DataTableConverter.ToDataTable(values);
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

        //public int GetTypeOfFinishingSelectedIndex(DataRow row)
        //{
        //    List<IHandbookDB> typeOfFinishing = DataManager.GetInstance().TypeOfFinishingDB_Repository.Read().ToList();

        //    return typeOfFinishing.FindIndex((elem) => elem.Id == _dataDictionary[row].TypeOfFinishingId);
        //}



        public void Add(uint contractId, DateTime dateOfPayment,
            float amountOfPayment)
        {
            DataManager.GetInstance().PaymentDB_Repository.Create(new PaymentDB(
                contractId,
                dateOfPayment.ToString("yyyy-MM-dd"),
                amountOfPayment
                ));
        }

        public void Update(DataRow row, uint contractId, DateTime dateOfPayment,
            float amountOfPayment)
        {
            DataManager.GetInstance().PaymentDB_Repository.Update(new PaymentDB(
                _dataDictionary[row].Id,
                contractId,
                dateOfPayment.ToString("yyyy-MM-dd"),
                amountOfPayment
                ));
        }

        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PaymentDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

﻿using DataBase1WPF.DataBase.Entities.Handbook;
using DataBase1WPF.DataBase.Entities.Payment;
using DataBase1WPF.DataBase.Repositories;
using System.Data;

namespace DataBase1WPF.Models.Services.Tables.Payment
{
    /// <summary>
    /// Сервис для платежей
    /// </summary>
    public class PaymentService
    {
        private Dictionary<DataRow, IPaymentDB> _dataDictionary;
        /// <summary>
        /// Получение платежей в таблице DataTable по идентификатору договора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Поиск данных по таблице платежи
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
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

        

        /// <summary>
        /// Добавление платежа
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="dateOfPayment"></param>
        /// <param name="amountOfPayment"></param>
        public void Add(uint contractId, DateTime dateOfPayment,
            float amountOfPayment)
        {
            DataManager.GetInstance().PaymentDB_Repository.Create(new PaymentDB(
                contractId,
                dateOfPayment.ToString("yyyy-MM-dd"),
                amountOfPayment
                ));
        }


        /// <summary>
        /// Изменение платежа
        /// </summary>
        /// <param name="row"></param>
        /// <param name="contractId"></param>
        /// <param name="dateOfPayment"></param>
        /// <param name="amountOfPayment"></param>
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

        /// <summary>
        /// Удаление платежа
        /// </summary>
        /// <param name="row"></param>
        public void Delete(DataRow row)
        {
            DataManager.GetInstance().PaymentDB_Repository.Delete(_dataDictionary[row].Id);
        }
    }
}

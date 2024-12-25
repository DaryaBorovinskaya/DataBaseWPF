using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Order;
using DataBase1WPF.DataBase.Entities.Premise;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие с заказами из базы данных
    /// </summary>
    public class OrderDB_Repository : IRepositoryDB<IOrderDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового заказа в базу данных
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IOrderDB entity)
        {
            _query = $"insert into orders " +
                     $"(premise_id, rental_purpose_id, contract_id, begin_of_rent, " +
                     $"end_of_rent, rental_payment) " +
                     $"values ({entity.PremiseID}, {entity.RentalPurposeId}," +
                     $"{entity.ContractId}, '{entity.BeginOfRent}', '{entity.EndOfRent}'," +
                     $"{entity.RentalPayment})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о заказах из базы данных
        /// </summary>
        /// <returns></returns>
        public IList<IOrderDB> Read()
        {
            _query = " select * from orders;";
            IList<IOrderDB> result = new List<IOrderDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new OrderDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString().Substring(0, 10),
                    row[5].ToString().Substring(0, 10),
                    float.Parse(row[6].ToString())
                ));
            }
            return result;

        }

        /// <summary>
        /// Измменение данных заказа из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IOrderDB entity)
        {
            _query = $"update orders set " +
                     $"premise_id={entity.PremiseID}, rental_purpose_id={entity.RentalPurposeId}, " +
                     $"contract_id={entity.ContractId}, begin_of_rent='{entity.BeginOfRent}', " +
                     $"end_of_rent='{entity.EndOfRent}', rental_payment={entity.RentalPayment} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление заказа из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from orders where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Получение заказов по идентификатору договора
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public IList<IOrderDB> GetOrdersByContractId(uint contract_id)
        {
            _query = $"SELECT \r\n    rentapp.orders.id, rentapp.orders.contract_id,   COALESCE(rentapp.orders.premise_id, 0) AS premise_id, \r\n    COALESCE(rentapp.buildings.district_id, 0) AS district_id,\r\n    MAX(rentapp.districts.title) AS district_title,\r\n    COALESCE(rentapp.buildings.street_id, 0) AS street_id,\r\n    MAX(rentapp.streets.title) AS street_title,\r\n    COALESCE(rentapp.buildings.id, 0) AS building_id,\r\n    rentapp.buildings.house_number as house_number,\r\n    MAX(rentapp.premises.floor_number) AS floor_number,\r\n    MAX(rentapp.premises.premise_number) AS premise_number,\r\n    MAX(rentapp.premises.area) AS area,\r\n    COALESCE(rentapp.orders.rental_purpose_id, 0) AS rental_purpose_id,\r\n    MAX(rentapp.rental_purposes.title) AS rental_purpose_title,\r\n    rentapp.orders.begin_of_rent,\r\n    rentapp.orders.end_of_rent,\r\n    rentapp.orders.rental_payment \r\nFROM \r\n    rentapp.orders \r\nLEFT OUTER JOIN \r\n    rentapp.premises ON rentapp.orders.premise_id = rentapp.premises.id\r\nLEFT OUTER JOIN \r\n    rentapp.buildings ON rentapp.premises.building_id = rentapp.buildings.id\r\nLEFT OUTER JOIN \r\n    rentapp.districts ON rentapp.buildings.district_id = rentapp.districts.id\r\nLEFT OUTER JOIN \r\n    rentapp.streets ON rentapp.buildings.street_id = rentapp.streets.id\r\n\r\nLEFT OUTER JOIN \r\n    rentapp.rental_purposes ON rentapp.orders.rental_purpose_id = rentapp.rental_purposes.id\r\nLEFT OUTER JOIN \r\n    rentapp.contracts ON rentapp.orders.contract_id = rentapp.contracts.id\r\nwhere rentapp.contracts.id = {contract_id}\r\nGROUP BY \r\n    rentapp.orders.id,\r\n    rentapp.orders.begin_of_rent,\r\n    rentapp.orders.end_of_rent,\r\n    rentapp.orders.rental_payment\r\nORDER BY \r\n    rentapp.orders.id;\r\n\r\n";
            IList<IOrderDB> result = new List<IOrderDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new OrderDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    uint.Parse(row[5].ToString()),
                    row[6].ToString(),
                    uint.Parse(row[7].ToString()),
                    row[8].ToString(),
                    int.Parse(row[9].ToString()),
                    row[10].ToString(),
                    float.Parse(row[11].ToString()),
                    uint.Parse(row[12].ToString()),
                    row[13].ToString(),
                    row[14].ToString().Substring(0,10),
                    row[15].ToString().Substring(0, 10),
                    float.Parse(row[16].ToString())
                ));
            }
            return result;
        }
    }
}

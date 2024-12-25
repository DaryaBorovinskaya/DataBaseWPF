using DataBase1WPF.DataBase.Entities.Building;
using DataBase1WPF.DataBase.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    /// <summary>
    /// Взаимодействие со зданиями из базы данных
    /// </summary>
    public class BuildingDB_Repository : IRepositoryDB<IBuildingDB>
    {
        private string _query;
        private string _exception = string.Empty;

        /// <summary>
        /// Добавление нового здания в базу даныых
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IBuildingDB entity)
        {
            _query = $"insert into buildings " +
                     $"(district_id, street_id, house_number, number_of_floors," +
                     $" count_rental_premises, commandant_phone_number) " +
                     $"values ({entity.DistrictId}, {entity.StreetId}," +
                     $" '{entity.HouseNumber}', {entity.NumberOfFloors}," +
                     $"{entity.CountRentalPremises}, '{entity.CommandantPhoneNumber}')";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Чтение данных о зданиях из базы данных 
        /// </summary>
        /// <returns></returns>
        public IList<IBuildingDB> Read()
        {
            _query = "SELECT rentapp.buildings.id, COALESCE(rentapp.buildings.district_id, 0) AS district_id, " +
                " MAX(rentapp.districts.title) AS district_title, COALESCE(rentapp.buildings.street_id, 0) AS street_id, " +
                " MAX(rentapp.streets.title) AS street_title,  rentapp.buildings.house_number,   rentapp.buildings.number_of_floors,  " +
                "  rentapp.buildings.count_rental_premises,  rentapp.buildings.commandant_phone_number FROM   " +
                "  rentapp.buildings LEFT OUTER JOIN  rentapp.districts ON rentapp.buildings.district_id = rentapp.districts.id " +
                " LEFT OUTER JOIN  rentapp.streets ON rentapp.buildings.street_id = rentapp.streets.id  GROUP BY  rentapp.buildings.id, " +
                "   rentapp.buildings.house_number,  rentapp.buildings.number_of_floors,  rentapp.buildings.count_rental_premises,  " +
                "  rentapp.buildings.commandant_phone_number ORDER BY  rentapp.buildings.id;";

            IList<IBuildingDB> result = new List<IBuildingDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new BuildingDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    row[2].ToString(),
                    uint.Parse(row[3].ToString()),
                    row[4].ToString(),
                    row[5].ToString(),
                    uint.Parse(row[6].ToString()),
                    uint.Parse(row[7].ToString()),
                    row[8].ToString()
                ));
            }
            return result;
        }

        /// <summary>
        /// Изменение данных здания из базы данных
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IBuildingDB entity)
        {
            _query = $"update buildings set " +
                     $"district_id={entity.DistrictId}, street_id={entity.StreetId}, " +
                     $"house_number='{entity.HouseNumber}', number_of_floors={entity.NumberOfFloors}, " +
                     $"count_rental_premises={entity.CountRentalPremises}, " +
                     $"commandant_phone_number='{entity.CommandantPhoneNumber}' " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

        /// <summary>
        /// Удаление здания из базы данных
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            _query = $"delete from buildings where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }

         

    }
}

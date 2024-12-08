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
    public class BuildingDB_Repository : IRepositoryDB<IBuildingDB>
    {
        private string _query;
        private string _exception = string.Empty;
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
        public IList<IBuildingDB> Read()
        {
            _query = "select * from buildings";
            IList<IBuildingDB> result = new List<IBuildingDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new BuildingDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    uint.Parse(row[4].ToString()),
                    uint.Parse(row[5].ToString()),
                    row[6].ToString()
                ));
            }
            return result;
        }

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
        public void Delete(uint id)
        {
            _query = $"delete from buildings where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
    }
}

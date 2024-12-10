using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Premise;
using DataBase1WPF.DataBase.Entities.WorkRecordCard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Repositories
{
    public class PremiseDB_Repository : IRepositoryDB<IPremiseDB>
    {
        private string _query;
        private string _exception = string.Empty;
        public void Create(IPremiseDB entity)
        {
            _query = $"insert into premises " +
                     $"(building_id, type_of_finishing_id, premise_number," +
                     $"area, floor_number, availability_of_phone_number," +
                     $"temp_rental_payment) " +
                     $"values ({entity.BuildingID}, {entity.TypeOfFinishingId}," +
                     $"'{entity.PremiseNumber}', {entity.Area}, {entity.FloorNumber}," +
                     $"{entity.AvailabilityOfPhoneNumber}, {entity.TempRentalPayment})";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public IList<IPremiseDB> Read()
        {
            _query = "select * from premises";
            IList<IPremiseDB> result = new List<IPremiseDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PremiseDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    float.Parse(row[4].ToString()),
                    int.Parse(row[5].ToString()),
                    int.Parse(row[6].ToString()) == 1 ? true : false,
                    float.Parse(row[7].ToString())
                ));
            }
            return result;
        }

        public void Update(IPremiseDB entity)
        {
            _query = $"update premises set " +
                     $"building_id={entity.BuildingID}, type_of_finishing_id={entity.TypeOfFinishingId}, " +
                     $"premise_number='{entity.PremiseNumber}', " +
                     $"area={entity.Area}, floor_number={entity.FloorNumber}, " +
                     $"availability_of_phone_number={entity.AvailabilityOfPhoneNumber}," +
                     $"temp_rental_payment={entity.TempRentalPayment} " +
                     $"where id={entity.Id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }
        public void Delete(uint id)
        {
            _query = $"delete from premises where id={id}";
            RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
        }


        public IList<IPremiseDB> GetPremisesByBuildingId(uint building_id)
        {
            _query = $"select * from premises where building_id = {building_id}";
            IList<IPremiseDB> result = new List<IPremiseDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PremiseDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    float.Parse(row[4].ToString()),
                    int.Parse(row[5].ToString()),
                    int.Parse(row[6].ToString()) == 1 ? true : false,
                    float.Parse(row[7].ToString())
                ));
            }
            return result;
        }
    }
}

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
            throw new NotImplementedException();
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
            _query = $"SELECT  rentapp.premises.id, rentapp.premises.building_id, " +
                $" COALESCE(rentapp.premises.type_of_finishing_id, 0) AS type_of_finishing_id, " +
                $" MAX(rentapp.types_of_finishings.title) AS type_of_finishing_title, " +
                $"  rentapp.premises.premise_number, rentapp.premises.area," +
                $"  rentapp.premises.floor_number, rentapp.premises.availability_of_phone_number, " +
                $"   rentapp.premises.temp_rental_payment  FROM   rentapp.premises " +
                $"left outer join rentapp.buildings on rentapp.premises.building_id =  rentapp.buildings.id " +
                $" LEFT OUTER JOIN   rentapp.types_of_finishings ON " +
                $"rentapp.premises.type_of_finishing_id = rentapp.types_of_finishings.id  " +
                $" where rentapp.buildings.id = {building_id} " +
                $"  GROUP BY   rentapp.premises.id,   rentapp.premises.premise_number, rentapp.premises.area, " +
                $"  rentapp.premises.floor_number, " +
                $" rentapp.premises.availability_of_phone_number,  " +
                $" rentapp.premises.temp_rental_payment ORDER BY   rentapp.premises.id;"; 
            IList<IPremiseDB> result = new List<IPremiseDB>();
            DataTable dataTable = RentappSQLConnection.GetInstance().ExecuteRequest(_query, ref _exception);
            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new PremiseDB(
                    uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()),
                    uint.Parse(row[2].ToString()),
                    row[3].ToString(),
                    row[4].ToString(),
                    float.Parse(row[5].ToString()),
                    int.Parse(row[6].ToString()),
                    int.Parse(row[7].ToString()) == 1 ? true : false,
                    float.Parse(row[8].ToString())
                ));
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Order
{
    public interface IOrderDB
    {
        public uint Id { get; set; }
        public uint ContractId { get; set; }
        public uint PremiseID { get; set; }
        
        public uint DistrictId { get; set; }

        [DisplayName("Район")]
        public string DistrictTitle { get; set; }
        
        public uint StreetId { get; set; }
        [DisplayName("Улица")]
        public string StreetTitle { get; set; }
        
        public uint BuildingId { get; set; }

        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }


        [DisplayName("Номер этажа")]
        public int FloorNumber { get; set; }

        [DisplayName("Номер помещения")]
        public string PremiseNumber { get; set; }

        [DisplayName("Площадь")]
        public float Area { get; set; }


        public uint RentalPurposeId { get; set; }

        [DisplayName("Цель аренды")]
        public string RentalPurposeTitle { get; set; }


        [DisplayName("Начало срока аренды")]
        public string/*DateTime*/ BeginOfRent { get; set; }

        [DisplayName("Конец срока аренды")]
        public string/*DateTime*/ EndOfRent { get; set; }

        [DisplayName("Арендная плата")]
        public float RentalPayment { get; set; }

        
    }
}

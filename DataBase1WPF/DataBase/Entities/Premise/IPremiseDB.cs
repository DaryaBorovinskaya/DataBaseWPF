using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Premise
{
    public interface IPremiseDB
    {
        public uint Id { get; set; }
        public uint BuildingID { get; set; }
        public uint TypeOfFinishingId { get; set; }
        [DisplayName("Вид отделки")]
        public string TypeOfFinishingTitle { get; set; }

        [DisplayName("Номер помещения")]
        public string PremiseNumber { get; set; }

        [DisplayName("Площадь")]
        public float Area { get; set; }

        [DisplayName("Номер этажа")]
        public int FloorNumber { get; set; }

        [DisplayName("Наличие телефона")]
        public bool AvailabilityOfPhoneNumber {  get; set; }

        [DisplayName("Текущая арендная плата")]
        public float TempRentalPayment { get; set; }
    }
}

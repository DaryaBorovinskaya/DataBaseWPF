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
        public uint PremiseID { get; set; }
        public uint RentalPurposeId { get; set; }
        public uint ContractId { get; set; }

        [DisplayName("Начало срока аренды")]
        public DateTime BeginOfRent { get; set; }

        [DisplayName("Конец срока аренды")]
        public DateTime EndOfRent { get; set; }

        [DisplayName("Арендная плата")]
        public float RentalPayment { get; set; }

        
    }
}

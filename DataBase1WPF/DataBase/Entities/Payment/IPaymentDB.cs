using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Payment
{
    public interface IPaymentDB
    {
        public uint Id {  get; set; }
        public uint IndividualId { get; set; }
        public uint JuridicalPersonId { get; set; }
        public uint ContractId { get; set; }

        [DisplayName("Дата платежа")]
        public DateTime DateOfPayment { get; set; }

        [DisplayName("Сумма платежа")]
        public float AmountOfPayment { get; set; }
    }
}

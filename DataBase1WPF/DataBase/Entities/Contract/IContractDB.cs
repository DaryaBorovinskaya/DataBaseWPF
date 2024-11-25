using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Contract
{
    public interface IContractDB
    {
        public uint Id { get; set; }
        public uint IndividualId { get; set; }
        public uint JuridicalPersonId { get; set; }
        public uint EmployeeId { get; set; }
        public uint PaymentFrequencyId { get; set; }

        [DisplayName("Регистрационный номер")]
        public string RegistrationNumber { get; set; }

        [DisplayName("Начало действия")]
        public DateOnly BeginOfAction {  get; set; }

        [DisplayName("Конец действия")]
        public DateOnly EndOfAction { get; set; }

        [DisplayName("Доп. условия")]
        public string AdditionalConditions {  get; set; }

        [DisplayName("Штраф")]
        public float Fine { get; set; }


    }
}

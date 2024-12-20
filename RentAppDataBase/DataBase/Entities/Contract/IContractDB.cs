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
        public uint? IndividualId { get; set; }

        [DisplayName("Фамилия физического лица")]
        public string IndividualSurname { get; set; }

        [DisplayName("Имя физического лица")]
        public string IndividualName { get; set; }

        [DisplayName("Отчество физического лица")]
        public string IndividualPatronymic { get; set; }
        public uint? JuridicalPersonId { get; set; }

        [DisplayName("Имя юридического лица")]
        public string JuridicalPersonName { get; set; }
        public uint EmployeeId { get; set; }


        [DisplayName("Регистрационный номер")]
        public string RegistrationNumber { get; set; }

        [DisplayName("Начало действия")]
        public string/*DateTime*/ BeginOfAction {  get; set; }

        [DisplayName("Конец действия")]
        public string/*DateTime*/ EndOfAction { get; set; }


        public uint PaymentFrequencyId { get; set; }

        [DisplayName("Периодичность оплаты")]
        public string PaymentFrequencyTitle { get; set; }


        [DisplayName("Фамилия сотрудника")]
        public string EmployeeSurname { get; set; }

        [DisplayName("Имя сотрудника")]
        public string EmployeeName { get; set; }

        [DisplayName("Отчество сотрудника")]
        public string EmployeePatronymic { get; set; }

        [DisplayName("Дополнительные условия")]
        public string AdditionalConditions {  get; set; }

        [DisplayName("Штраф")]
        public float Fine { get; set; }


    }
}

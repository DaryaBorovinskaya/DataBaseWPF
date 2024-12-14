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

        [DisplayName("Фамилия физ. лица")]
        public string IndividualSurname { get; set; }

        [DisplayName("Имя физ. лица")]
        public string IndividualName { get; set; }

        [DisplayName("Отчество физ. лица")]
        public string IndividualPatronymic { get; set; }
        public uint JuridicalPersonId { get; set; }

        [DisplayName("Имя юр. лица")]
        public string JuridicalPersonName { get; set; }
        public uint EmployeeId { get; set; }


        [DisplayName("Регистр. номер")]
        public string RegistrationNumber { get; set; }

        [DisplayName("Начало действия")]
        public string/*DateTime*/ BeginOfAction {  get; set; }

        [DisplayName("Конец действия")]
        public string/*DateTime*/ EndOfAction { get; set; }


        public uint PaymentFrequencyId { get; set; }

        [DisplayName("Периодичн. оплаты")]
        public string PaymentFrequencyTitle { get; set; }


        [DisplayName("Фамилия сотрудника")]
        public string EmployeeSurname { get; set; }

        [DisplayName("Имя сотрудника")]
        public string EmployeeName { get; set; }

        [DisplayName("Отчество сотрудника")]
        public string EmployeePatronymic { get; set; }

        [DisplayName("Доп. условия")]
        public string AdditionalConditions {  get; set; }

        [DisplayName("Штраф")]
        public float Fine { get; set; }


    }
}

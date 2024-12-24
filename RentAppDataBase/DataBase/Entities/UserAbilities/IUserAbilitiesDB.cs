using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.UserAbilities
{
    public interface IUserAbilitiesDB
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }

        [DisplayName("Имя пользователя")]
        public string UserLogin { get; set; }

        public uint EmployeeId {  get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        public string? Patronymic { get; set; }
        public uint MenuElemId { get; set; }

        [DisplayName("Элемент меню")]
        public string MenuElemName { get; set; }

        [DisplayName("Чтение")]
        public bool R {  get; set; }

        [DisplayName("Добавление")]
        public bool W { get; set; }

        [DisplayName("Изменение")]
        public bool E { get; set; }

        [DisplayName("Удаление")]
        public bool D { get; set; }
    }
}

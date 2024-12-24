using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.User
{
    public interface IUserDB
    {
        public uint Id { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
        public uint EmployeeId { get; set; }
        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }
    }
}

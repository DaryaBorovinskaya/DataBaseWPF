using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.User
{
    public class UserDB : IUserDB
    {
        public uint Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public UserDB(uint id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public UserDB(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

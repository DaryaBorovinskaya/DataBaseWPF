using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.UserAbilities
{
    public class UserAbilitiesDB : IUserAbilitiesDB
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint MenuElemId { get; set; }

        public bool R { get; set; }

        public bool W { get; set; }

        public bool E { get; set; }

        public bool D { get; set; }
        public string UserLogin {  get; set; }
        public string MenuElemName {  get; set; }

        public UserAbilitiesDB(uint id, uint userId, string userLogin, uint menuElemId,
            string menuElemName, bool r, bool w, bool e, bool d)
        {
            Id = id;
            UserId = userId;
            UserLogin = userLogin;
            MenuElemId = menuElemId;
            MenuElemName = menuElemName;
            R = r;
            W = w;
            E = e;
            D = d;
        }


        public UserAbilitiesDB(uint id, uint userId, uint menuElemId, 
            bool r, bool w, bool e, bool d)
        {
            Id = id;
            UserId = userId;
            MenuElemId = menuElemId;
            R = r;
            W = w;
            E = e;
            D = d;
        }

        public UserAbilitiesDB(uint userId, uint menuElemId,
            bool r, bool w, bool e, bool d)
        {
            UserId = userId;
            MenuElemId = menuElemId;
            R = r;
            W = w;
            E = e;
            D = d;
        }
    }
}

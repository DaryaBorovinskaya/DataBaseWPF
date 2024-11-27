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
        public uint MenuElemId { get; set; }

        [DisplayName("R")]
        public bool R {  get; set; }

        [DisplayName("W")]
        public bool W { get; set; }

        [DisplayName("E")]
        public bool E { get; set; }

        [DisplayName("D")]
        public bool D { get; set; }
    }
}

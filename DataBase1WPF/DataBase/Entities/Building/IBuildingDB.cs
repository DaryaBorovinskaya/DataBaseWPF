using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase.Entities.Building
{
    public interface IBuildingDB
    {
        public uint Id { get; set; }
        public uint DistrictId { get; set; }
        public uint StreetId { get; set; }

        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }

        [DisplayName("Номер этажей")]
        public uint NumberOfFloors { get; set; }

        [DisplayName("Кол-во помещений для аренды")]
        public uint CountRentalPremises { get; set; }


        [DisplayName("Телефон коменданта")]
        public string CommandantPhoneNumber { get; set; }
    }
}

using System.ComponentModel;

namespace DataBase1WPF.DataBase.Entities.Building
{
    public interface IBuildingDB
    {
        public uint Id { get; set; }
        [DisplayName("Район")]
        public string DistrictTitle { get; set; }
        public uint DistrictId { get; set; }
        [DisplayName("Улица")]
        public string StreetTitle { get; set; }
        public uint StreetId { get; set; }

        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }

        [DisplayName("Кол-во этажей")]
        public uint NumberOfFloors { get; set; }

        [DisplayName("Кол-во помещений для аренды")]
        public uint CountRentalPremises { get; set; }


        [DisplayName("Телефон коменданта")]
        public string CommandantPhoneNumber { get; set; }

        

        
    }
}

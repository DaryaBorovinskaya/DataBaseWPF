namespace DataBase1WPF.DataBase.Entities.UserAbilities
{
    /// <summary>
    /// Данные о правах пользователей из базы данных
    /// </summary>
    public class UserAbilitiesDB : IUserAbilitiesDB
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public string UserLogin { get; set; }
        public uint EmployeeId { get; set; }
        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }
        public uint MenuElemId { get; set; }

        public bool R { get; set; }

        public bool W { get; set; }

        public bool E { get; set; }

        public bool D { get; set; }
        
        public string MenuElemName {  get; set; }

        public UserAbilitiesDB(uint id, uint userId, string userLogin,
            uint employeeId, string surname, string name, string? patronymic,
            uint menuElemId,
            string menuElemName, bool r, bool w, bool e, bool d)
        {
            Id = id;
            UserId = userId;
            UserLogin = userLogin;
            EmployeeId = employeeId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
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

        public UserAbilitiesDB(uint userId,  uint menuElemId,
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

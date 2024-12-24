namespace DataBase1WPF.DataBase.Entities.User
{
    public class UserDB : IUserDB
    {
        public uint Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
        public uint EmployeeId { get; set; }
        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }

        public UserDB(uint id, string login, string password, uint employeeId,
            string surname, string name, string? patronymic)
        {
            Id = id;
            Login = login;
            Password = password;
            EmployeeId = employeeId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }


        public UserDB(uint id, string login, string password, uint employeeId)
        {
            Id = id;
            Login = login;
            Password = password;
            EmployeeId = employeeId;
        }

        public UserDB(string login, string password, uint employeeId)
        {
            Login = login;
            Password = password;
            EmployeeId = employeeId;
        }
    }
}

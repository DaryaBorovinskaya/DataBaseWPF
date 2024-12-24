using DataBase1WPF.DataBase.Entities.Employee;
using DataBase1WPF.DataBase.Entities.User;
using DataBase1WPF.Models.Encryptors;

namespace DataBase1WPF.Models.Services.Registration
{
    public class RegistrationService 
    {
        private IEncryptor<string,  string> _encryptor;

        public RegistrationService(IEncryptor<string, string> encryptor)
        {
            _encryptor = encryptor;
        }

        public List<string> GetEmployees()
        {
            List<string> employees = new();

            List<IEmployeeDB> employeesDB = DataManager.GetInstance().EmployeeDB_Repository.Read().ToList();

            foreach (IEmployeeDB employeeDB in employeesDB)
                employees.Add(employeeDB.Surname + " " + employeeDB.Name + " " + employeeDB.Patronymic);


            return employees;
        }

        public bool Registration(string login, string password, int employeeSelectedIndex)
        {
            IList<IUserDB> usersDB = DataManager.GetInstance().UserDB_Repository.Read();
            foreach (IUserDB userDB in usersDB)
            {
                if (userDB.Login == login)
                    return false;
            }

            DataManager.GetInstance().UserDB_Repository.Create(new UserDB(
                login,
                _encryptor.Encrypt(password),
                DataManager.GetInstance().EmployeeDB_Repository.Read().ToList()[employeeSelectedIndex].Id));
            return true;
        }
    }
}

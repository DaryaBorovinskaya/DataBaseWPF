using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Registration
{
    public interface IRegistrationService
    {
        bool Registration(string login, string password);
    }
}

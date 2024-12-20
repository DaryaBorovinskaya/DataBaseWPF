using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.ChangePassword
{
    public interface IChangePasswordService
    {
        public void ChangePassword(string oldPassword, string newPassword);
    }
}

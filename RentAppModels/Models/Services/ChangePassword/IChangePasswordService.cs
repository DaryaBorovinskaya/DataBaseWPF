namespace DataBase1WPF.Models.Services.ChangePassword
{
    public interface IChangePasswordService
    {
        public void ChangePassword(string oldPassword, string newPassword);
    }
}

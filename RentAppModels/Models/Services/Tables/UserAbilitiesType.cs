namespace DataBase1WPF.Models.Services.Tables
{
    /// <summary>
    /// Виды прав пользователей
    /// </summary>
    public class UserAbilitiesType
    {
        public bool CanRead {  get; set; }
        public bool CanWrite { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}

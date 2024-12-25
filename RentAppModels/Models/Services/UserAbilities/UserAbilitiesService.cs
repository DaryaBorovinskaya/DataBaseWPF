using DataBase1WPF.DataBase.Entities.MenuElem;
using DataBase1WPF.DataBase.Entities.UserAbilities;

namespace DataBase1WPF.Models.Services.UserAbilities
{
    /// <summary>
    /// Сервис прав пользователей 
    /// </summary>
    public class UserAbilitiesService
    {
        private IList<IMenuElemDB> _menuElems;
        private IList<IUserAbilitiesDB> _userAbilities;

        public UserAbilitiesService() 
        {
            _menuElems = DataManager.GetInstance().MenuElemDB_Repository.Read();
            _userAbilities = new List<IUserAbilitiesDB>();
            IList<IUserAbilitiesDB> userAbilitiesDB = DataManager.GetInstance().UserAbilitiesDB_Repository.Read();
            foreach(IUserAbilitiesDB userAbilities in userAbilitiesDB)
            {
                if (userAbilities.UserId == DataManager.GetInstance().CurrentUser.Id)
                {
                    _userAbilities.Add(userAbilities);
                }
            }
        }

        /// <summary>
        /// Получение прав пользователей  
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IUserAbilitiesDB GetUserAbilitiesByFuncName(string funcName) 
        {
            foreach(IMenuElemDB menuElem in _menuElems) 
            {
                if (menuElem.FuncName == funcName)
                {
                    foreach(IUserAbilitiesDB userAbilities in _userAbilities)
                    {
                        if (userAbilities.MenuElemId == menuElem.Id)
                            return userAbilities;
                    }
                    throw new ArgumentException($"Для пользователя " +
                        $"{DataManager.GetInstance().CurrentUser.Login} " +
                        $"отсутствуют права доступа к пункту меню {menuElem.Name}");

                }
            }
            throw new ArgumentException($"Пункт меню с функцией {funcName} отсутствует");
        }
    }
}

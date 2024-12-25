namespace DataBase1WPF.Models.Services.MenuElems
{
    /// <summary>
    /// Сервис для элементов меню
    /// </summary>
    public class MenuElemsService : IMenuElemsService
    {
        /// <summary>
        /// Получение идентификатора элемента меню
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public uint GetCurrentMenuElemId(string functionName)
        {
            return DataManager.GetInstance().MenuElemDB_Repository.Read().ToList().Where(
                elem => elem.FuncName == functionName
                ).ToList()[0].Id;
            
        }
    }
}

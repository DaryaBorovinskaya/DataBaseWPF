using DataBase1WPF.ViewModels;
using RentAppModels.Models.Services;

namespace RentAppViewModels.ViewModels
{
    /// <summary>
    /// Обработка и получение данных из окна ContentWindow
    /// </summary>
    public class ContentVM : BasicVM
    {
        private string _contentText;
        private ContentService _contentService;
        public string ContentText
        {
            get { return _contentText; }
        }

        public ContentVM()
        {
            _contentService = new();
            _contentText = _contentService.ContentText;
        }
    }
}

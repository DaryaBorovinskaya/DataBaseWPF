using DataBase1WPF.ViewModels;
using RentAppModels.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAppViewModels.ViewModels
{
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

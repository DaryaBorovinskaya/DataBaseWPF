using DataBase1WPF.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.ViewModels
{
    public class AboutProgramVM : BasicVM
    {
        private string _aboutProgramText;
        private AboutProgramService _aboutProgramService;
        public string AboutProgramText
        {
            get { return _aboutProgramText; }
        }

        public AboutProgramVM()
        {
            _aboutProgramService = new();
            _aboutProgramText = _aboutProgramService.AboutProgramText;
        }
    }
}

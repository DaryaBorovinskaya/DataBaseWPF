﻿using DataBase1WPF.Models.Services;

namespace DataBase1WPF.ViewModels
{
    /// <summary>
    /// Обработка и получение данных из окна AboutProgramWindow
    /// </summary>
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

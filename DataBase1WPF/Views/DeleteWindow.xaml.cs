﻿using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.Handbooks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        private Window _confirmWindow;
        public DeleteWindow(AddEditDeleteEnum addEditDelete, ITableService tableService, Window window, DataRow row=null, string confirmText=null )
        {

            InitializeComponent();
            DataContext = new DeleteHandbookVM(addEditDelete, tableService, row, confirmText);
            if (DataContext is DeleteHandbookVM deleteVM)
            {
                _confirmWindow = window;
                deleteVM.OnExit += Exit;
                deleteVM.OnApply += Apply;
            }
        }

        public void Apply()
        {
            if (_confirmWindow.DataContext is HandbooksVM handbooksVM)
            {
                handbooksVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddHandbookVM addHandbookVM)
            {
                addHandbookVM.Add();
            }
            else if (_confirmWindow.DataContext is EditHandbookVM editHandbookVM)
            {
                editHandbookVM.Edit();
            }
        }
        public void Exit()
        {
            Close();
        }
    }
}

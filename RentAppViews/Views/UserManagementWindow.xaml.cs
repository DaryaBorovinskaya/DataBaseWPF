﻿using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels.UserManagementVM;
using DataBase1WPF.Views.AddOrEdit;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        public UserManagementWindow(ITableService tableService, uint menuElemId)
        {
            InitializeComponent();
            DataContext = new UserManagementVM(tableService, menuElemId);
            if (DataContext is UserManagementVM userManagementVM)
            {
                userManagementVM.OnAdd += Add;
                userManagementVM.OnEdit += Edit;
                userManagementVM.OnDelete += Delete;
            }
        }

        /// <summary>
        /// Обработчик события Добавление 
        /// </summary>
        /// <param name="tableService"></param>
        public void Add(ITableService tableService)
        {
            AddOrEditUserManagementWindow window = new(AddEditDeleteEnum.Add, tableService, this);
            window.ShowDialog();
        }

        /// <summary>
        /// Обработчик события Изменение 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Edit(DataRow row, ITableService tableService)
        {
            AddOrEditUserManagementWindow window = new(AddEditDeleteEnum.Edit, tableService, this, row);
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события Удаление 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableService"></param>
        public void Delete(DataRow row, ITableService tableService)
        {
            ConfirmWindow window = new(AddEditDeleteEnum.Delete, tableService, this, " пользователь " + row[0].ToString() 
                + " элемент меню " + row[4].ToString());
            window.ShowDialog();
        }


        /// <summary>
        /// Обработчик события нажатия на элемент DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is UserManagementVM userManagementVM)
                userManagementVM.DataTableMouseDown();
        }

        /// <summary>
        /// Обработчик события нажатия на элемент TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is UserManagementVM userManagementVM)
                userManagementVM.DataTableMouseLeave();
        }
    }
}

﻿using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
using DataBase1WPF.ViewModels.Building;
using DataBase1WPF.ViewModels.Contract;
using DataBase1WPF.ViewModels.Employee;
using DataBase1WPF.ViewModels.Handbooks;
using DataBase1WPF.ViewModels.Individual;
using DataBase1WPF.ViewModels.JuridicalPerson;
using DataBase1WPF.ViewModels.UserManagement;
using DataBase1WPF.ViewModels.UserManagementVM;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        private Window _confirmWindow;
        private OtherTablesEnum _otherTables;
        public ConfirmWindow(AddEditDeleteEnum addEditDelete, ITableService tableService, Window window,  string confirmText=null,
            OtherTablesEnum otherTables = OtherTablesEnum.None)
        {

            InitializeComponent();  
            DataContext = new ConfirmVM(
                addEditDelete == AddEditDeleteEnum.Add 
                ? ViewModels.AddEditDeleteEnum.Add
                : (addEditDelete == AddEditDeleteEnum.Edit
                    ? ViewModels.AddEditDeleteEnum.Edit
                    : ViewModels.AddEditDeleteEnum.Delete)
                , tableService, confirmText);
            if (DataContext is ConfirmVM deleteVM)
            {
                _confirmWindow = window;
                _otherTables = otherTables;
                deleteVM.OnExit += Exit;
                deleteVM.OnApply += Apply;
            }
        }


        /// <summary>
        /// Обработчик события Применить
        /// </summary>
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

            else if (_confirmWindow.DataContext is BuildingVM buildingVM)
            {
                if (_otherTables == OtherTablesEnum.Premises)
                    buildingVM.DeletePremises();
                else
                    buildingVM.Delete();
            }

            else if (_confirmWindow.DataContext is AddBuildingVM addBuildingVM)
            {
                addBuildingVM.Add();
            }
            else if (_confirmWindow.DataContext is EditBuildingVM editBuildingVM)
            {
                editBuildingVM.Edit();
            }

            else if (_confirmWindow.DataContext is AddPremiseVM addPremiseVM)
            {
                addPremiseVM.Add();
            }
            else if (_confirmWindow.DataContext is EditPremiseVM editPremiseVM)
            {
                editPremiseVM.Edit();
            }
            else if (_confirmWindow.DataContext is EmployeeVM employeeVM)
            {
                if (_otherTables == OtherTablesEnum.WorkRecordCard)
                    employeeVM.DeleteWorkRecordCard();
                else
                    employeeVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddEmployeeVM addEmployeeVM)
            {
                addEmployeeVM.Add();
            }
            else if (_confirmWindow.DataContext is EditEmployeeVM editEmployeeVM)
            {
                editEmployeeVM.Edit();
            }
            else if (_confirmWindow.DataContext is AddWorkRecordCardVM addWorkRecordCardVM)
            {
                addWorkRecordCardVM.Add();
            }
            else if (_confirmWindow.DataContext is EditWorkRecordCardVM editWorkRecordCardVM)
            {
                editWorkRecordCardVM.Edit();
            }
            else if (_confirmWindow.DataContext is IndividualVM individualVM)
            {
                individualVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddIndividualVM addIndividualVM)
            {
                addIndividualVM.Add();
            }
            else if (_confirmWindow.DataContext is EditIndividualVM editIndividualVM)
            {
                editIndividualVM.Edit();
            }
            else if (_confirmWindow.DataContext is JuridicalPersonVM juridicalPersonVM)
            {
                juridicalPersonVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddJuridicalPersonVM addJuridicalPersonVM)
            {
                addJuridicalPersonVM.Add();
            }
            else if (_confirmWindow.DataContext is EditJuridicalPersonVM editJuridicalPersonVM)
            {
                editJuridicalPersonVM.Edit();
            }
            else if (_confirmWindow.DataContext is ContractVM contractVM)
            {
                if (_otherTables == OtherTablesEnum.Orders)
                    contractVM.DeleteOrder();
                else if (_otherTables == OtherTablesEnum.Payments)
                    contractVM.DeletePayment();
                else
                    contractVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddContractVM addContractVM)
            {
                addContractVM.Add();
            }
            else if (_confirmWindow.DataContext is EditContractVM editContractVM)
            {
                editContractVM.Edit();
            }
            else if (_confirmWindow.DataContext is AddOrderVM addOrderVM)
            {
                addOrderVM.Add();
            }
            else if (_confirmWindow.DataContext is EditOrderVM editOrderVM)
            {
                editOrderVM.Edit();
            }
            else if (_confirmWindow.DataContext is AddPaymentVM addPaymentVM)
            {
                addPaymentVM.Add();
            }
            else if (_confirmWindow.DataContext is EditPaymentVM editPaymentVM)
            {
                editPaymentVM.Edit();
            }
            else if (_confirmWindow.DataContext is UserManagementVM userManagementVM)
            {
                userManagementVM.Delete();
            }
            else if (_confirmWindow.DataContext is AddUserManagementVM addUserManagementVM)
            {
                addUserManagementVM.Add();
            }
            else if (_confirmWindow.DataContext is EditUserManagementVM editUserManagementVM)
            {
                editUserManagementVM.Edit();
            }
        }

        /// <summary>
        /// Обработчик события Выход
        /// </summary>
        public void Exit()
        {
            Close();
        }
    }
}

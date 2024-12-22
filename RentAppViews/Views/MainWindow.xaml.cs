using DataBase1WPF.Models.Services.Tables;
using DataBase1WPF.ViewModels;
using DataBase1WPF.Views.Constructors;
using RentAppViews.Views;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MenuConstructor menuConstructor = new();
            constructedMenu.Children.Add(menuConstructor.Construct());
            DataContext = new MainVM();
            if (DataContext is MainVM mainVM)
            {
                mainVM.OnRegistration += Registration;
                mainVM.OnUserManagement += UserManagement;
                mainVM.OnChangePassword += ChangePassword;
                mainVM.OnSQLquery += SQLquery;
                mainVM.OnExport += Export;

                mainVM.OnDistricts += Handbooks;
                mainVM.OnStreets += Handbooks;
                mainVM.OnBanks += Handbooks;
                mainVM.OnPositions += Handbooks;
                mainVM.OnPaymentFrequency += Handbooks;
                mainVM.OnRentalPurposes += Handbooks;
                mainVM.OnTypesOfFinishing += Handbooks;
                mainVM.OnFine += Handbooks;

                mainVM.OnIndividuals += Individual;
                mainVM.OnJuridicalPersons += JuridicalPerson;
                mainVM.OnBuilding += Building;
                mainVM.OnEmployees += Employee;

                mainVM.OnAboutProgram += AboutProgram;
                mainVM.OnContent += Content;
            }
        }

        private void Registration()
        {
            RegistrationWindow window = new();
            window.ShowDialog();
        }
        private void UserManagement(ITableService tableService, uint menuElemId)
        {
            UserManagementWindow window = new( tableService,  menuElemId);
            window.ShowDialog();
            
        }

        private void Content()
        {
            ContentWindow window = new();
            window.ShowDialog();

        }

        private void Export(ITableService tableService, uint menuElemId)
        {
            ExportWindow window = new(tableService, menuElemId);
            window.ShowDialog();

        }



        private void ChangePassword()
        {
            ChangePasswordWindow window = new();
            window.ShowDialog();
        }

        private void SQLquery()
        {
            SQLqueryWindow window = new ();
            window.Show();
        }

        private void Handbooks(ITableService tableService, uint menuElemId)
        {
            HandbooksWindow window = new(tableService, menuElemId);
            window.ShowDialog();

        }

        private void Individual(ITableService tableService, uint menuElemId)
        {
            IndividualWindow window = new(tableService, menuElemId);
            window.ShowDialog();
        }

        private void JuridicalPerson(ITableService tableService, uint menuElemId)
        {
            JuridicalPersonWindow window = new(tableService, menuElemId);
            window.ShowDialog();
        }


        private void Building(ITableService tableService, uint menuElemId)
        {
            BuildingWindow window = new(tableService, menuElemId);
            window.ShowDialog();
        }

        private void Employee(ITableService tableService, uint menuElemId)
        {
            EmployeesWindow window = new(tableService, menuElemId);
            window.ShowDialog();
        }

        private void AboutProgram()
        {
            AboutProgramWindow window = new();
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

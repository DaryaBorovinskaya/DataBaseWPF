using DataBase1WPF.ViewModels;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using DataBase1WPF.Views.Constructors;
using System.Windows.Data;
using DataBase1WPF.Models.Services.Tables;

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

            if (DataContext is MainVM mainVM)
            {
                mainVM.OnRegistration += Registration;
                mainVM.OnChangePassword += ChangePassword;
                mainVM.OnSQLquery += SQLquery;

                mainVM.OnDistricts += Handbooks;
                mainVM.OnStreets += Handbooks;
                mainVM.OnBanks += Handbooks;
                mainVM.OnPositions += Handbooks;
                mainVM.OnPaymentFrequency += Handbooks;
                mainVM.OnRentalPurposes += Handbooks;
                mainVM.OnTypesOfFinishing += Handbooks;
                mainVM.OnFine += Handbooks;

            }
        }

        private void Registration()
        {
            RegistrationWindow window = new();
            window.Show();
            
        }

        private void ChangePassword()
        {
            ChangePasswordWindow window = new();
            window.Show();
        }

        private void SQLquery()
        {
            SQLqueryWindow window = new ();
            window.Show();
        }

        private void Handbooks(ITableService tableService)
        {
            HandbooksWindow window = new(tableService);
            window.Show();



            //DataGrid dataGrid = new();
            //Binding binding = new ("DataTableHandbooks");
            //binding.Source = DataContext is MainVM;
            //dataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding);
             
        }
        


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

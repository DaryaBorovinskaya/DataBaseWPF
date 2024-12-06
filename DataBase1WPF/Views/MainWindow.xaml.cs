using DataBase1WPF.ViewModels;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using DataBase1WPF.Views.Constructors;
using System.Windows.Data;

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

                mainVM.OnDistricts += Districts;
                mainVM.OnStreets += Streets;
                mainVM.OnBanks += Banks;
                mainVM.OnPositions += Positions;
                mainVM.OnPaymentFrequency += PaymentFrequency;
                mainVM.OnRentalPurposes += RentalPurposes;
                mainVM.OnTypesOfFinishing += TypesOfFinishing;
                mainVM.OnFine += Fine;

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

        private void Districts()
        {
            HandbooksWindow window = new(HandbooksEnum.Districts);
            window.Show();



            //DataGrid dataGrid = new();
            //Binding binding = new ("DataTableHandbooks");
            //binding.Source = DataContext is MainVM;
            //dataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding);
             
        }
        private void Streets()
        {
            HandbooksWindow window = new(HandbooksEnum.Streets);
            window.Show();
        }
        private void Banks()
        {
            HandbooksWindow window = new(HandbooksEnum.Banks);
            window.Show();
        }
        private void Positions()
        {
            HandbooksWindow window = new(HandbooksEnum.Positions);
            window.Show();
        }
        private void PaymentFrequency()
        {
            HandbooksWindow window = new(HandbooksEnum.PaymentFrequency);
            window.Show();
        }
        private void RentalPurposes()
        {
            HandbooksWindow window = new(HandbooksEnum.RentalPurposes);
            window.Show();
        }
        private void TypesOfFinishing()
        {
            HandbooksWindow window = new(HandbooksEnum.TypesOfFinishing);
            window.Show();
        }
        private void Fine()
        {
            HandbooksWindow window = new(HandbooksEnum.Fine);
            window.Show();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

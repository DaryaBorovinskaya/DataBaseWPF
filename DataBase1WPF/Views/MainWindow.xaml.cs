using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql;
using MySql.Data.MySqlClient;
using DataBase1WPF.ViewModels;

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

            /*string connectionString = "SERVER=localhost;DATABASE=rentapp;UID=root;PASSWORD=humanrazum0425;";
        
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("select * from banks", connection);

            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            connection.Close();

            dataSQL.DataContext = dt;*/
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

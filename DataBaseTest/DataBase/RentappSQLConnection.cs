using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.DataBase
{ 
    public class RentappSQLConnection
    {
        private readonly string _connectionString = "SERVER=localhost;DATABASE=rentapp;UID=root;PASSWORD=root;";
        private readonly MySqlConnection _connection;

        private static RentappSQLConnection _instance;

        private RentappSQLConnection()
        {
            _connection = new(_connectionString);
        }

        public static RentappSQLConnection GetInstance()
        {
            if (_instance == null)
                _instance = new RentappSQLConnection();
            return _instance;
        }

        public DataTable ExecuteRequest(string query, ref string exception)
        {
            _connection.Open();

            MySqlCommand command = new(query, _connection);

            MySqlDataReader reader;
            try
            {
                reader = command.ExecuteReader();
            }

            catch (Exception ex)
            {
                exception = ex.Message;
                _connection.Close();
                return null;
            }

            exception = string.Empty;
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            _connection.Close();
            return dataTable;
        }


    }
}

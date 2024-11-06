using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models
{
    public class RentappSQLConnection
    {
        private readonly string _connectionString = "SERVER=localhost;DATABASE=rentapp;UID=root;PASSWORD=humanrazum0425;";
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

        public MySqlConnection CreateConnection()
        {
            return _connection;
            /*using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Заполняем коллекцию данных
                    _banks.Add(new Banks
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    });
                }
            }

            _connection.Close();
            }*/
        }
        
    }
}

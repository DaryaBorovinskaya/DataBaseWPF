using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models
{
    //public class Handbook
    //{
    //    private string _exceptionText;
    //    public string Title { get; set; }

    //    public string GetExceptionText()
    //    {
    //        return _exceptionText;
    //    }

    //    public DataTable GetData(string query, out string exception)
    //    //public ObservableCollection<Handbook> GetData(string query, out string exception)
    //    {
            
    //        RentappSQLConnection db_connection = RentappSQLConnection.GetInstance();

    //        MySqlConnection connection = db_connection.CreateConnection();
            
    //        connection.Open();

    //        MySqlCommand command = new(query, connection);

    //        MySqlDataReader reader;
    //        try
    //        {
    //            reader = command.ExecuteReader();
    //        }

    //        catch (MySqlException ex) 
    //        {
    //            exception = ex.Message;
    //            connection.Close();
    //            return null;
    //            /*command.CommandText = "select title from banks";
    //            reader = command.ExecuteReader();*/
    //        }
            
                
            
    //        ObservableCollection<Handbook> _handbooks = new();

    //        //while (reader.Read())
    //        //{
    //        //    _handbooks.Add(new Handbook
    //        //    {
    //        //        //Id = reader.GetInt32("id"),
    //        //        Title = reader.GetString("title")
    //        //    });
                
    //        //}
            
    //        exception = string.Empty;
    //        DataTable dt = new DataTable();
    //        dt.Load(reader);
    //        connection.Close();
    //        return dt;
    //        //return _handbooks;
    //    }
    //}
}

using DataBase1WPF.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DataBase1WPF.DataBase;

namespace DataBase1WPF.ViewModels
{
    public class MainVM : BasicVM
    {
        //private Handbook _handbooks = new();
        //private ObservableCollection<Handbook> _handbookTable = new();
        private DataTable _dataTable = new();
        private string _query;
        private string _errorMessage;

        //public ObservableCollection<Handbook> HandbookTable
        //{
        //    get
        //    {
        //        //_banks = _banksDB.GetData();
        //        /*string connectionString = "SERVER=localhost;DATABASE=rentapp;UID=root;PASSWORD=humanrazum0425;";

        //        using (MySqlConnection connection = new(connectionString))
        //        {
        //            connection.Open();

        //            MySqlCommand command = new("select * from banks", connection);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Заполняем коллекцию данных
        //                    _banks.Add(new Banks
        //                    {
        //                        Id = reader.GetInt32("id"),
        //                        Name = reader.GetString("name")
        //                    });
        //                }
        //            }

        //            connection.Close();
        //        }*/
        //        return _handbookTable;
        //    }

        //    set
        //    {
        //        Set<ObservableCollection<Handbook>>(ref _handbookTable, value);
        //    }

        //}

        public DataTable DataBaseTable 
        {
            get { return _dataTable; }
            set
            {
                Set(ref _dataTable, value);
            }
        }

        public string Query
        {
            get { return _query; }
            set 
            {
                Set<string>(ref _query, value); 

            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                Set<string>(ref _errorMessage, value);
            }
        }

        public ICommand ClickExecuteQuery
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string exception = string.Empty;
                    DataTable table = RentappSQLConnection.GetInstance().ExecuteRequest(
                        Query, ref exception
                    ); 
                    //DataTable table = _handbooks.GetData(Query, out exception); ;
                    //ObservableCollection<Handbook> testingHandbook = _handbooks.GetData(Query, out exception);
                    
                    if (exception != string.Empty) 
                    {
                        DataBaseTable.Clear();
                        //HandbookTable.Clear();
                        MessageBox.Show(exception);
                        
                    }
                    else 
                    {
                        //HandbookTable = testingHandbook;
                        DataBaseTable = table;
                        
                    }
                    
                    
                });
            }
        }

    }
}

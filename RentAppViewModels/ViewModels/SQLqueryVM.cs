using DataBase1WPF.DataBase;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace DataBase1WPF.ViewModels
{
    public class SQLqueryVM : BasicVM
    {
        private DataTable _dataTable = new();
        private string _query;

       
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
        
        public ICommand ClickExecuteQuery
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (Query == null || Query == string.Empty)
                        MessageBox.Show("Пустой запрос");
                    else
                    {
                        string exception = string.Empty;
                        DataTable table = RentappSQLConnection.GetInstance().ExecuteRequest(
                            Query, ref exception
                        );
                        
                        if (exception != string.Empty)
                        {
                            DataBaseTable.Clear();
                            MessageBox.Show(exception);

                        }
                        else
                        {
                            DataBaseTable = table;

                        }
                    }
                    
                });
            }
        }

    }
}

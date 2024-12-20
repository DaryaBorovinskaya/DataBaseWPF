using DataBase1WPF.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using DataBase1WPF.DataBase;

namespace DataBase1WPF.ViewModels
{
    public class SQLqueryVM : BasicVM
    {
        private DataTable _dataTable = new();
        private string _query;
        //private string _errorMessage;

       
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
        #region ForMessageWindow
        /*public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                Set<string>(ref _errorMessage, value);
            }
        }*/
        #endregion
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
                    }
                    
                });
            }
        }

    }
}

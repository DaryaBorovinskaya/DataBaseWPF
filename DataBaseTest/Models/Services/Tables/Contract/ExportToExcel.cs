using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase1WPF.Models.Services.Tables.Contract
{
    public class ExportToExcel 
    {
        public static void ExportTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataTable.Columns.Count + 1; i++)
                {
                    excel.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        excel.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                    }
                }

                excel.Columns.AutoFit();
                excel.Visible = true;
            }
        }
    }
}

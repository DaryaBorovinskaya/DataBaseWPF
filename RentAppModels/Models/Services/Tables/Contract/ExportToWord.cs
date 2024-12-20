using DataBase1WPF.DataBase.Entities.Contract;
using DataBase1WPF.DataBase.Entities.JuridicalPerson;
using DataBase1WPF.DataBase.Entities.Order;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataBase1WPF.Models.Services.Tables.Contract
{
    public class ExportToWord
    {
        public static void ExportContract(IContractDB contract, System.Data.DataTable orders, 
            IJuridicalPersonDB juridicalPersonDB = null)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            Document doc = wordApp.Documents.Open(@"C:\Users\dashh\source\repos\DataBaseWPF\RentAppResources\Resources\Договор.docx");
            //Document doc = wordApp.Documents.Open(@"/Resources/Договор.docx");
            wordApp.Visible = true;

            if (contract.IndividualId != 0)
                FindAndReplace(wordApp, "{TenantName}", contract.IndividualSurname + " "
                    + contract.IndividualName + " " + contract.IndividualPatronymic);
            else if (contract.JuridicalPersonId != 0)
                FindAndReplace(wordApp, "{TenantName}", juridicalPersonDB.DirectorSurname + " "
                    + juridicalPersonDB.DirectorName + " " + juridicalPersonDB.DirectorPatronymic);

            FindAndReplace(wordApp, "{LandlordName}", contract.EmployeeSurname + " "
                    + contract.EmployeeName + " " + contract.EmployeePatronymic);

            FindAndReplace(wordApp, "{RegistrationNumber}", contract.RegistrationNumber);
            FindAndReplace(wordApp, "{Fine}", contract.Fine);

            if (contract.AdditionalConditions != null && contract.AdditionalConditions != string.Empty)
                FindAndReplace(wordApp, "{AdditionalConditions}", contract.AdditionalConditions);
            else
                FindAndReplace(wordApp, "{AdditionalConditions}", "отсутствуют");

            FindAndReplace(wordApp, "{PaymentFrequencyTitle}", contract.PaymentFrequencyTitle);
            if (orders == null)
                FindAndReplace(wordApp, "{Orders}", " ");
            else
                FindAndReplaceTable(doc, "{Orders}", orders);
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            Microsoft.Office.Interop.Word.Range range = wordApp.ActiveDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: ref findText,
                               ReplaceWith: ref replaceWithText,
                               Replace: WdReplace.wdReplaceAll);
        }

        static void FindAndReplaceTable(Document doc, string findText, System.Data.DataTable dataTable)
        {
            Microsoft.Office.Interop.Word.Range range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(findText);

            if (range.Find.Found)
            {
                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);

                if (dataTable.Rows.Count > 0)
                {
                    float fontSize = 14;
                    if (dataTable.Columns.Count > 8)
                    {
                        fontSize = 10;
                    }

                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
                        table.Cell(1, i + 1).Range.Font.Size = fontSize;
                    }

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString();
                            table.Cell(i + 2, j + 1).Range.Font.Size = fontSize;
                        }
                    }

                    table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                    range.Text = "";
                }
            }
        }

    }
}

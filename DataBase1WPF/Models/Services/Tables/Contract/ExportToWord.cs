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
        public static void ExportTable(System.Data.DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                float fontSize = 11;
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                word.Application.Documents.Add(Type.Missing);

                if (dataTable.Columns.Count > 8)
                {
                    fontSize = 8;
                    word.Application.ActiveDocument.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                }
                Microsoft.Office.Interop.Word.Table table = word.Application.ActiveDocument.Tables.Add(word.Selection.Range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

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

                word.Visible = true;
            }
        }


        public static void ExportContract(System.Data.DataTable dataTable)
        {
            string landlordName = "Иванов Иван Иванович";
            string tenantName = "Петров Петр Петрович";
            string propertyAddress = "г. Москва, ул. Ленина, д. 1";
            string leaseTerm = "12 месяцев";
            string rentAmount = "30000";

            // Создание экземпляра приложения Word
            Application wordApp = new Application();

            Document doc = wordApp.Documents.Open(@"C:\Users\dashh\source\repos\DataBaseWPF\DataBase1WPF\Resources\Договор.docx");
            //Document doc = wordApp.Documents.Open(@"\Resources\Договор.docx");
            wordApp.Visible = true;



            // Замена меток на данные
            FindAndReplace(wordApp, "{TenantName}", dataTable.Rows[0][0].ToString() + " "
                + dataTable.Rows[0][1].ToString() + " " + dataTable.Rows[0][2].ToString());
            FindAndReplace(wordApp, "{LandlordName}", dataTable.Rows[0][8].ToString() + " "
                + dataTable.Rows[0][9].ToString() + " " + dataTable.Rows[0][10].ToString());
            FindAndReplace(wordApp, "{RegistrationNumber}", dataTable.Rows[0][4].ToString());
            /*FindAndReplace(wordApp, "{LandlordName}", dataTable.Rows[0][0].ToString());
            FindAndReplace(wordApp, "{TenantName}", dataTable.Rows[0][1].ToString());
            FindAndReplace(wordApp, "{PropertyAddress}", dataTable.Rows[0][2].ToString());
            FindAndReplace(wordApp, "{LeaseTerm}", dataTable.Rows[0][3].ToString());
            FindAndReplace(wordApp, "{RentAmount}", dataTable.Rows[0][4].ToString());*/

            // Сохранение документа
            //object savePath = @"C:\Path\To\Your\FilledAgreement.docx";
            //doc.SaveAs2(ref savePath);

            // Закрытие документа и приложения Word
            //doc.Close();
            //wordApp.Quit();
        }

        private static void FindAndReplace(Application wordApp, object findText, object replaceWithText)
        {
            Microsoft.Office.Interop.Word.Range range = wordApp.ActiveDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: ref findText,
                               ReplaceWith: ref replaceWithText,
                               Replace: WdReplace.wdReplaceAll);
        }
    }
}

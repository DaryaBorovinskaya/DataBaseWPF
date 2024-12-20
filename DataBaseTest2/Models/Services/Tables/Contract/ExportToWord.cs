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

        

        public static void ExportContract(IContractDB contract, System.Data.DataTable orders, 
            IJuridicalPersonDB juridicalPersonDB = null)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            Document doc = wordApp.Documents.Open(@"C:\Users\dashh\source\repos\DataBaseWPF\DataBase1WPF\Resources\Договор.docx");
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
                FindAndReplaceTable(wordApp, doc, "{Orders}", orders);
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            Microsoft.Office.Interop.Word.Range range = wordApp.ActiveDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: ref findText,
                               ReplaceWith: ref replaceWithText,
                               Replace: WdReplace.wdReplaceAll);
        }

        static void FindAndReplaceTable(Microsoft.Office.Interop.Word.Application wordApp,
    Document doc, string findText, System.Data.DataTable dataTable)
        {
            // Найти текст и заменить его на таблицу
            Microsoft.Office.Interop.Word.Range range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(findText);

            // Проверка, найден ли текст
            if (range.Find.Found)
            {
                // Создать таблицу в месте найденного текста
                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(range, dataTable.Rows.Count + 1, dataTable.Columns.Count, Type.Missing, Type.Missing);

                if (dataTable.Rows.Count > 0)
                {
                    float fontSize = 14;
                    if (dataTable.Columns.Count > 8)
                    {
                        fontSize = 10;
                    }

                    // Заполнение заголовков таблицы
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        table.Cell(1, i + 1).Range.Text = dataTable.Columns[i].ColumnName;
                        table.Cell(1, i + 1).Range.Font.Size = fontSize;
                    }

                    // Заполнение данных таблицы
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            table.Cell(i + 2, j + 1).Range.Text = dataTable.Rows[i][j].ToString();
                            table.Cell(i + 2, j + 1).Range.Font.Size = fontSize;
                        }
                    }

                    // Установка стилей границ таблицы
                    table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                    // Удаляем оригинальный текст
                    range.Text = "";
                }
            }
        }

    }
}

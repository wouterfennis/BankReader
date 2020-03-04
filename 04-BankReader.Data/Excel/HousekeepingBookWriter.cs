using BankReader.Data.Excel.Extensions;
using BankReader.Implementation.Models;
using BankReader.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankReader.Data.Excel
{
    public class HousekeepingBookWriter : IHousekeepingBookWriter
    {
        private string[] _headerColumns = {
            "Categorie",
            "Januari",
            "Februari",
            "Maart",
            "April",
            "Mei",
            "Juni",
            "Juli",
            "Augustus",
            "September",
            "Oktober",
            "November",
            "December",
            "Totaal per jaar",
            "Gemiddeld per maand"
        };

        public void Write(IEnumerable<HouseholdPost> houseHoldPosts)
        {
            IEnumerable<HouseholdPost> expenses = houseHoldPosts.Where(t => t.Transactions.All(x => x.TransactionDirection == TransactionDirection.Af));
            IEnumerable<HouseholdPost> income = houseHoldPosts.Where(t => t.Transactions.All(x => x.TransactionDirection == TransactionDirection.Bij));

            //Creates a blank workbook. Use the using statement, so the package is disposed when we are done.
            using (var excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Huishoudboek");

                var worksheetWriter = new ExcelWorksheetWriter(worksheet);

                worksheetWriter
                    .SetColor(System.Drawing.Color.Red)
                    .Write("Uitgaven")
                    .MoveDown();

                foreach (string headerColumn in _headerColumns)
                {
                    worksheetWriter
                        .SetColor(System.Drawing.Color.WhiteSmoke)
                        .Write(headerColumn)
                        .MoveRight();
                }

                worksheetWriter
                    .NewLine()
                    .SetColor(System.Drawing.Color.White);
                PrintTransacties(expenses, worksheetWriter);

                worksheetWriter.NewLine()
                    .NewLine();

                worksheetWriter
                    .SetColor(System.Drawing.Color.Green)
                    .Write("Inkomsten")
                    .MoveDown();

                foreach (string headerColumn in _headerColumns)
                {
                    worksheetWriter
                        .SetColor(System.Drawing.Color.WhiteSmoke)
                        .Write(headerColumn)
                        .MoveRight();
                }

                worksheetWriter
                    .NewLine()
                    .SetColor(System.Drawing.Color.White);
                PrintTransacties(income, worksheetWriter);

                excelPackage.SaveAs(new FileInfo(@"C:\Git\BankReader\test.xlsx"));
            }
        }

        private void PrintTransacties(IEnumerable<HouseholdPost> householdPosts, IWorksheetWriter worksheetWriter)
        {
            int topIndex = worksheetWriter.CurrentPosition.Y;
            foreach (var householdPost in householdPosts)
            {
                worksheetWriter.Write(householdPost.Category.ToString());

                foreach (var householdTransaction in householdPost.Transactions)
                {
                    var maandXIndex = BepaalMaandIndex(householdTransaction.Date);
                    worksheetWriter.CurrentPosition = new System.Drawing.Point(maandXIndex, worksheetWriter.CurrentPosition.Y);
                    worksheetWriter.Write(householdTransaction.Amount);
                }
                worksheetWriter.CurrentPosition = new System.Drawing.Point(14, worksheetWriter.CurrentPosition.Y);
                worksheetWriter.SetColor(System.Drawing.Color.LightYellow)
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .SetColor(System.Drawing.Color.White)
                .NewLine();
            }

            worksheetWriter.CurrentPosition = new System.Drawing.Point(1, worksheetWriter.CurrentPosition.Y);
            worksheetWriter.Write("Totaal per maand");

            for (int i = 2; i < 14; i++)
            {
                //TODO: remove, below is needed to convert value to euro
                worksheetWriter.CurrentPosition = new System.Drawing.Point(i, worksheetWriter.CurrentPosition.Y);
                worksheetWriter
                    .SetColor(System.Drawing.Color.LightYellow)
                    .PlaceFormula(new System.Drawing.Point(i, topIndex), new System.Drawing.Point(i, worksheetWriter.CurrentPosition.Y - 1), FormulaType.SUM);
            }
        }

        private int BepaalMaandIndex(DateTime datum)
        {
            return datum.Month + 1;
        }
    }
}

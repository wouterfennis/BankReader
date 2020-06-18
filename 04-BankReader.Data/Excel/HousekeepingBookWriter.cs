using BankReader.Data.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankReader.Data.Excel
{
    public class HousekeepingBookWriter : IHousekeepingBookWriter
    {
        private readonly string[] _headerColumns = {
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

        private readonly IReadOnlyList<YearMonth> monthsOfYear;

        public HousekeepingBookWriter()
        {
            monthsOfYear = Enumerable.Range(1, 12).Select(i => new YearMonth(DateTime.Now.Year, i)).ToList();

        }

        public void Write(HouseholdBook householdBook)
        {
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



                PrintExpenses(householdBook.HouseholdPosts, worksheetWriter);

                worksheetWriter.NewLine()
                    .NewLine();

                //worksheetWriter
                //    .SetColor(System.Drawing.Color.Green)
                //    .Write("Inkomsten")
                //    .MoveDown();

                //foreach (string headerColumn in _headerColumns)
                //{
                //    worksheetWriter
                //        .SetColor(System.Drawing.Color.WhiteSmoke)
                //        .Write(headerColumn)
                //        .MoveRight();
                //}

                //worksheetWriter
                //    .NewLine()
                //    .SetColor(System.Drawing.Color.White);
                //PrintTransactions(income, worksheetWriter);

                excelPackage.SaveAs(new FileInfo(@"C:\Git\BankReader\test.xlsx"));
            }
        }

        private void PrintExpenses(IEnumerable<HouseholdPost> householdPosts, IWorksheetWriter worksheetWriter)
        {
            int topIndex = worksheetWriter.CurrentPosition.Y;
            foreach (var householdPost in householdPosts)
            {
                worksheetWriter.Write(householdPost.Category.ToString());

                foreach (var yearMonth in monthsOfYear)
                {
                    foreach (var transaction in householdPost.GetExpenses(yearMonth))
                    {
                        var monthXIndex = yearMonth.Month.Value;
                        worksheetWriter.CurrentPosition = new System.Drawing.Point(monthXIndex, worksheetWriter.CurrentPosition.Y);
                        worksheetWriter.Write(transaction.Amount);
                    }

                    worksheetWriter.CurrentPosition = new System.Drawing.Point(14, worksheetWriter.CurrentPosition.Y);
                    worksheetWriter.SetColor(System.Drawing.Color.LightYellow)
                    .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                    .MoveRight()
                    .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                    .SetColor(System.Drawing.Color.White)
                    .NewLine();
                }
            }

            worksheetWriter.CurrentPosition = new System.Drawing.Point(1, worksheetWriter.CurrentPosition.Y);
            worksheetWriter.Write("Totaal per maand");

            for (int i = 2; i < 14; i++)
            {
                worksheetWriter.CurrentPosition = new System.Drawing.Point(i, worksheetWriter.CurrentPosition.Y);
                worksheetWriter
                    .SetColor(System.Drawing.Color.LightYellow)
                    .PlaceFormula(new System.Drawing.Point(i, topIndex), new System.Drawing.Point(i, worksheetWriter.CurrentPosition.Y - 1), FormulaType.SUM);
            }
        }
    }
}

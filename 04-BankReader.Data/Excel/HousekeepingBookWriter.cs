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
            "Category",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
            "Total a year",
            "Average a month"
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
                    .SetBackgroundColor(System.Drawing.Color.DimGray)
                    .Write("Householdposts")
                    .MoveDown();

                foreach (string headerColumn in _headerColumns)
                {
                    worksheetWriter
                        .SetBackgroundColor(System.Drawing.Color.WhiteSmoke)
                        .Write(headerColumn)
                        .MoveRight();
                }

                worksheetWriter
                    .NewLine();

                foreach (var householdPost in householdBook.HouseholdPosts)
                {
                    PrintHouseholdPost(householdPost, worksheetWriter);
                }

                excelPackage.SaveAs(new FileInfo(@"C:\Git\BankReader\test.xlsx"));
            }
        }

        private void PrintHouseholdPost(HouseholdPost householdPost, IWorksheetWriter worksheetWriter)
        {
            int topIndex = worksheetWriter.CurrentPosition.Y;
            worksheetWriter
                .SetBackgroundColor(System.Drawing.Color.LightYellow)
                .Write(householdPost.Category.ToString())
                .MoveRight();

            var year = DateTime.Now.Year;
            for (int monthNumber = 1; monthNumber < 13; monthNumber++)
            {
                var yearMonth = new YearMonth(year, monthNumber);
                var incomeInMonth = householdPost.GetIncome(yearMonth);
                var expensesInMonth = householdPost.GetExpenses(yearMonth);

                worksheetWriter
                    .SetBackgroundColor(System.Drawing.Color.Salmon)
                    .SetFontColor(System.Drawing.Color.Black)
                    .Write(-1 * expensesInMonth)
                    .MoveDown()
                    .SetBackgroundColor(System.Drawing.Color.LightGreen)
                    .Write(incomeInMonth)
                    .MoveDown()
                    .SetBackgroundColor(System.Drawing.Color.Gainsboro)
                    .SetFontColor(System.Drawing.Color.Black)
                    .PlaceFormula(new System.Drawing.Point(worksheetWriter.CurrentPosition.X, topIndex), new System.Drawing.Point(worksheetWriter.CurrentPosition.X, worksheetWriter.CurrentPosition.Y -1), FormulaType.SUM)
                    .MoveRight()
                    .MoveUp()
                    .MoveUp();
            }

            worksheetWriter
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(worksheetWriter.CurrentPosition.X - 1, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(worksheetWriter.CurrentPosition.X - 2, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .MoveDown()
                .MoveLeft()
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(worksheetWriter.CurrentPosition.X - 1, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(worksheetWriter.CurrentPosition.X - 2, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .NewLine()
                .NewLine()
                .NewLine();
        }
    }
}

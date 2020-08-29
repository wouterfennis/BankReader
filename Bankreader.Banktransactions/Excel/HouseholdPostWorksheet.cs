using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.Domain.Models;
using OfficeOpenXml;
using System;
using System.Drawing;

namespace Bankreader.FileSystem.Excel
{
    public class HouseholdPostWorksheet
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

        private readonly ExcelWorksheet _worksheet;

        public HouseholdPostWorksheet(ExcelWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public void Write(HouseholdBook householdBook)
        {
            var worksheetWriter = new ExcelWorksheetWriter(_worksheet);

            WriteTitle(worksheetWriter);

            WriteHeaderRow(worksheetWriter);

            foreach (var householdPost in householdBook.HouseholdPosts)
            {
                PrintHouseholdPost(householdPost, worksheetWriter);
            }

        }

        private void WriteTitle(ExcelWorksheetWriter worksheetWriter)
        {
            worksheetWriter
                .SetBackgroundColor(Color.DimGray)
                .Write("Householdposts")
                .MoveDown();
        }

        private void WriteHeaderRow(IWorksheetWriter worksheetWriter)
        {
            foreach (string headerColumn in _headerColumns)
            {
                worksheetWriter
                    .SetBackgroundColor(Color.WhiteSmoke)
                    .Write(headerColumn)
                    .MoveRight();
            }

            worksheetWriter
                .NewLine();
        }

        private void PrintHouseholdPost(HouseholdPost householdPost, IWorksheetWriter worksheetWriter)
        {
            int topIndex = worksheetWriter.CurrentPosition.Y;
            worksheetWriter
                .SetBackgroundColor(Color.LightYellow)
                .Write(householdPost.Category.ToString())
                .MoveRight();

            var year = DateTime.Now.Year;
            for (int monthNumber = 1; monthNumber < 13; monthNumber++)
            {
                var yearMonth = new YearMonth(year, monthNumber);
                var incomeInMonth = householdPost.GetIncome(yearMonth);
                var expensesInMonth = householdPost.GetExpenses(yearMonth);

                worksheetWriter
                    .SetBackgroundColor(Color.FromArgb(255,199,206))
                    .SetFontColor(Color.Black)
                    .Write(-1 * expensesInMonth)
                    .MoveDown()
                    .SetBackgroundColor(Color.LightGreen)
                    .Write(incomeInMonth)
                    .MoveDown()
                    .SetBackgroundColor(Color.Gainsboro)
                    .SetFontColor(Color.Black)
                    .PlaceFormula(new Point(worksheetWriter.CurrentPosition.X, topIndex), new Point(worksheetWriter.CurrentPosition.X, worksheetWriter.CurrentPosition.Y - 1), FormulaType.SUM)
                    .MoveRight()
                    .MoveUp()
                    .MoveUp();
            }

            worksheetWriter
                .PlaceFormula(new Point(2, worksheetWriter.CurrentPosition.Y), new Point(worksheetWriter.CurrentPosition.X - 1, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new Point(2, worksheetWriter.CurrentPosition.Y), new Point(worksheetWriter.CurrentPosition.X - 2, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .MoveDown()
                .MoveLeft()
                .PlaceFormula(new Point(2, worksheetWriter.CurrentPosition.Y), new Point(worksheetWriter.CurrentPosition.X - 1, worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new Point(2, worksheetWriter.CurrentPosition.Y), new Point(worksheetWriter.CurrentPosition.X - 2, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .NewLine()
                .NewLine()
                .NewLine();
        }
    }
}

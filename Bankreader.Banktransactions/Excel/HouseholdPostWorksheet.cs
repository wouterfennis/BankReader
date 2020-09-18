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

        private readonly ExcelWorksheetWriter _worksheetWriter;

        public HouseholdPostWorksheet(ExcelWorksheet worksheet)
        {
            _worksheetWriter = new ExcelWorksheetWriter(worksheet);
        }

        public void Write(HouseholdBook householdBook)
        {
            WriteTitle();

            WriteHeaderRow();

            foreach (var householdPost in householdBook.HouseholdPosts)
            {
                PrintHouseholdPost(householdPost);
            }
        }

        private void WriteTitle()
        {
            _worksheetWriter
                .SetBackgroundColor(Color.DimGray)
                .Write("Householdposts")
                .MoveDown();
        }

        private void WriteHeaderRow()
        {
            foreach (string headerColumn in _headerColumns)
            {
                _worksheetWriter
                    .SetBackgroundColor(Color.WhiteSmoke)
                    .Write(headerColumn)
                    .MoveRight();
            }

            _worksheetWriter
                .NewLine();
        }

        private void PrintHouseholdPost(HouseholdPost householdPost)
        {
            int topIndex = _worksheetWriter.CurrentPosition.Y;
            _worksheetWriter
                .SetBackgroundColor(Color.LightYellow)
                .Write(householdPost.Category.ToString())
                .MoveRight();

            var year = DateTime.Now.Year;
            for (int monthNumber = 1; monthNumber < 13; monthNumber++)
            {
                var yearMonth = new YearMonth(year, monthNumber);
                var incomeInMonth = householdPost.GetIncome(yearMonth);
                var expensesInMonth = householdPost.GetExpenses(yearMonth);

                _worksheetWriter
                    .SetBackgroundColor(Color.FromArgb(255,199,206))
                    .SetFontColor(Color.Black)
                    .Write(-1 * expensesInMonth)
                    .MoveDown()
                    .SetBackgroundColor(Color.LightGreen)
                    .Write(incomeInMonth)
                    .MoveDown()
                    .SetBackgroundColor(Color.Gainsboro)
                    .SetFontColor(Color.Black)
                    .PlaceFormula(new Point(_worksheetWriter.CurrentPosition.X, topIndex), new Point(_worksheetWriter.CurrentPosition.X, _worksheetWriter.CurrentPosition.Y - 1), FormulaType.SUM)
                    .MoveRight()
                    .MoveUp()
                    .MoveUp();
            }

            _worksheetWriter
                .PlaceFormula(new Point(2, _worksheetWriter.CurrentPosition.Y), new Point(_worksheetWriter.CurrentPosition.X - 1, _worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new Point(2, _worksheetWriter.CurrentPosition.Y), new Point(_worksheetWriter.CurrentPosition.X - 2, _worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .MoveDown()
                .MoveLeft()
                .PlaceFormula(new Point(2, _worksheetWriter.CurrentPosition.Y), new Point(_worksheetWriter.CurrentPosition.X - 1, _worksheetWriter.CurrentPosition.Y), FormulaType.SUM)
                .MoveRight()
                .PlaceFormula(new Point(2, _worksheetWriter.CurrentPosition.Y), new Point(_worksheetWriter.CurrentPosition.X - 2, _worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE)
                .NewLine()
                .NewLine()
                .NewLine();
        }
    }
}

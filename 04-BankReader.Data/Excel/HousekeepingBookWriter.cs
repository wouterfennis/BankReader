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
                //A workbook must have at least one cell, so lets add one... 
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Huishoudboek");
                var writer = new ExcelWriter(worksheet, 1, 1);
                writer.Write("Uitgaven");
                writer.SetBackgroundColor(244, 60, 50);

                PrintHeader(writer);

                //PrintTransacties(writer, expenses);
                writer.MoveDown()
                    .MoveDown()
                    .NewLine()
                    .Write("Inkomsten")
                    .SetBackgroundColor(60, 200, 30)
                    .MoveDown();

                PrintHeader(writer);
                yIndex = PrintTransacties(income, worksheet, yIndex);

                excelPackage.SaveAs(new FileInfo(@"C:\Users\woute\Downloads\test.xlsx"));
            }
        }

        private int PrintTransacties(IEnumerable<HouseholdPost> householdPosts, ExcelWorksheet worksheet, int yIndex)
        {
            int topIndex = yIndex;
            foreach (var householdPost in householdPosts)
            {
                var transactieRow = worksheet.SelectedRange[yIndex, 1, yIndex, 15];
                transactieRow.ConvertToEuro();

                worksheet.Cells[yIndex, 1].Value = householdPost.Category;

                foreach (var householdTransaction in householdPost.Transactions)
                {
                    var maandXIndex = BepaalMaandIndex(householdTransaction.Date);
                    var cell = worksheet.Cells[yIndex, maandXIndex];
                    cell.Value = householdTransaction.Amount;
                }
                worksheet.Cells[yIndex, 14].SetSumFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);
                worksheet.Cells[yIndex, 15].SetAverageFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);

                transactieRow.AutoFitColumns();
                yIndex++;
            }

            var monthTotalRow = worksheet.SelectedRange[yIndex, 1, yIndex, 15];
            monthTotalRow.ConvertToEuro();

            worksheet.Cells[yIndex, 1].Value = "Totaal per maand";
            for (int i = 2; i < 14; i++)
            {
                var totalPerMonthCell = worksheet.Cells[yIndex, i];
                totalPerMonthCell.ConvertToEuro();
                totalPerMonthCell.SetSumFormula(worksheet.Cells[topIndex, i], worksheet.Cells[yIndex - 1, i]);
                totalPerMonthCell.SetBackgroundColor(105, 185, 235);
            }
            // Jaar totaal
            worksheet.Cells[yIndex, 14].SetSumFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);
            worksheet.Cells[yIndex, 15].SetAverageFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);
            monthTotalRow.AutoFitColumns();

            return yIndex;
        }

        private int BepaalMaandIndex(DateTime datum)
        {
            return datum.Month + 1;
        }

        private void PrintHeader(ExcelWriter writer)
        {
            writer.NewLine();
            foreach (string headerColumn in _headerColumns)
            {
                writer.Write(headerColumn)
                .SetBackgroundColor(215, 220, 225)
                .MoveRight();
            }
            writer.MoveDown();
        }
    }
}

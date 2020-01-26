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

                var worksheetWriter = new ExcelWorksheetWriter(worksheet);

                worksheetWriter
                    .Write("Uitgaven")
                    .SetColor(System.Drawing.Color.Red)
                    .MoveDown();

                foreach (string headerColumn in _headerColumns)
                {
                    worksheetWriter
                        .Write(headerColumn)
                        .SetColor(System.Drawing.Color.Red)
                        .MoveRight();
                    //var cell = worksheet.Cells[yIndex, xIndex];
                    //cell.Value = headerColumn;
                    //cell.SetBackgroundColor(215, 220, 225);
                    //cell.AutoFitColumns();
                    //xIndex++;
                }

                worksheetWriter.MoveDown();
                //                yIndex++;

                //int yIndex = 1;
                //var expensesHeaderCell = worksheet.Cells[yIndex, 1];
                //expensesHeaderCell.Value = "Uitgaven";
                //expensesHeaderCell.SetBackgroundColor(244, 60, 50);

                //    yIndex = PrintHeader(worksheet, yIndex);

                PrintTransacties(expenses, worksheetWriter);
                //yIndex = yIndex + 2;

                //var incomeHeaderCell = worksheet.Cells[yIndex, 1];
                //incomeHeaderCell.Value = "Inkomsten";
                //incomeHeaderCell.SetBackgroundColor(60, 200, 30);

                //yIndex++;
                //yIndex = PrintHeader(worksheet, yIndex);
                //yIndex = PrintTransacties(income, worksheet, yIndex);

                excelPackage.SaveAs(new FileInfo(@"C:\Users\woute\Downloads\test.xlsx"));
            }
        }

        private void PrintTransacties(IEnumerable<HouseholdPost> householdPosts, IWorksheetWriter worksheetWriter)
        {
            int topIndex = worksheetWriter.CurrentPosition.Y;
            foreach (var householdPost in householdPosts)
            {
                worksheetWriter.Write(householdPost.Category.ToString());
                //worksheet.Cells[yIndex, 1].Value = householdPost.Category;

                foreach (var householdTransaction in householdPost.Transactions)
                {
                    var maandXIndex = BepaalMaandIndex(householdTransaction.Date);
                    worksheetWriter.CurrentPosition = new System.Drawing.Point(maandXIndex, worksheetWriter.CurrentPosition.Y);
                    //var cell = worksheet.Cells[yIndex, maandXIndex];
                    worksheetWriter.Write(householdTransaction.Amount);
                    //cell.Value = householdTransaction.Amount;
                }
                worksheetWriter.PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(14, worksheetWriter.CurrentPosition.Y), FormulaType.SUM);
                worksheetWriter.PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(15, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE);
                //    worksheet.Cells[yIndex, 14].SetSumFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);
                //    worksheet.Cells[yIndex, 15].SetAverageFormula(worksheet.Cells[yIndex, 2], worksheet.Cells[yIndex, 13]);
                worksheetWriter.MoveDown();
                //   yIndex++;
            }

            worksheetWriter.CurrentPosition = new System.Drawing.Point(worksheetWriter.CurrentPosition.Y, 1);
            worksheetWriter.Write("Totaal per maand");

            //worksheet.Cells[yIndex, 1].Value = "Totaal per maand";
            for (int i = 2; i < 14; i++)
            {
                worksheetWriter.PlaceFormula(new System.Drawing.Point(i, topIndex), new System.Drawing.Point(i, worksheetWriter.CurrentPosition.Y - 1), new System.Drawing.Point(worksheetWriter.CurrentPosition.Y, i), FormulaType.SUM);

              //  totalPerMonthCell.SetSumFormula(worksheet.Cells[topIndex, i], worksheet.Cells[yIndex - 1, i]);
            }
            // Jaar totaal

            worksheetWriter.PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(14, worksheetWriter.CurrentPosition.Y), FormulaType.SUM);
            worksheetWriter.PlaceFormula(new System.Drawing.Point(2, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(13, worksheetWriter.CurrentPosition.Y), new System.Drawing.Point(15, worksheetWriter.CurrentPosition.Y), FormulaType.AVERAGE);

         //   return yIndex;
        }

        private int BepaalMaandIndex(DateTime datum)
        {
            return datum.Month + 1;
        }

        //private int PrintHeader(ExcelWorksheet worksheet, int yIndex)
        //{

        //}
    }
}

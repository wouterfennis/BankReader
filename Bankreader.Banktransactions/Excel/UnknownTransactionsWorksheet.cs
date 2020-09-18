using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.Domain.Models;
using OfficeOpenXml;
using System;
using System.Drawing;

namespace Bankreader.FileSystem.Excel
{
    public class UnknownTransactionsWorksheet
    {
        private readonly string[] _headerColumns = {
            "Description",
            "Amount",
            "Income/Expense",
            "Date"
        };

        private readonly ExcelWorksheetWriter _worksheetWriter;

        public UnknownTransactionsWorksheet(ExcelWorksheet worksheet)
        {
            _worksheetWriter = new ExcelWorksheetWriter(worksheet);
        }

        public void Write(HouseholdBook householdBook)
        {
            WriteTitle(_worksheetWriter);

            WriteHeaderRow(_worksheetWriter);

            var unknownHousholdPost = householdBook.RetrieveHouseholdPost(Category.Unknown);
            foreach (var transaction in unknownHousholdPost.OriginalTransactions)
            {
                PrintTransaction(transaction);
            }
        }

        private void WriteTitle(ExcelWorksheetWriter worksheetWriter)
        {
            worksheetWriter
                .SetBackgroundColor(Color.DimGray)
                .Write("Unknown Transactions")
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

        private void PrintTransaction(Transaction transaction)
        {
            _worksheetWriter
                .SetBackgroundColor(Color.LightYellow)
                .Write(transaction.Description)
                .MoveRight()
                .Write(transaction.Amount)
                .MoveRight()
                .Write(transaction.TransactionDirection.ToString())
                .MoveRight()
                .Write(transaction.YearMonth.ToString())
                .NewLine();
        }
    }
}

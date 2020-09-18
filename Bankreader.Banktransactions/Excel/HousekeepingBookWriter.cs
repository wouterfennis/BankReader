using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;

namespace Bankreader.FileSystem.Excel
{
    // TODO: In Application layer?
    //This is more than just excel
    public sealed class HousekeepingBookWriter : IHousekeepingBookWriter
    {
        private readonly ExcelPackage _excelPackage;

        public HousekeepingBookWriter(IFileLocationProvider fileLocationProvider)
        {
            IFileInfoWrapper workbookLocation = fileLocationProvider.GetWorkbookLocation();
            _excelPackage = new ExcelPackage(workbookLocation.ToFileInfo());
        }

        public async Task WriteAsync(HouseholdBook householdBook)
        {
            _excelPackage.Workbook.Properties.Author = "W. Fennis";
            _excelPackage.Workbook.Properties.Title = "Householdbook";
            _excelPackage.Workbook.Properties.Subject = "Export";
            _excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet householdBookWorksheet = _excelPackage.Workbook.Worksheets.Add("Householdbook");

            var householdPostWorksheet = new HouseholdPostWorksheet(householdBookWorksheet);
            householdPostWorksheet.Write(householdBook);

            ExcelWorksheet unknownTransactions = _excelPackage.Workbook.Worksheets.Add("Unknown Transactions");
            var unknownTransactionsWorksheet = new UnknownTransactionsWorksheet(unknownTransactions);
            unknownTransactionsWorksheet.Write(householdBook);

            await _excelPackage.SaveAsync();
        }

        public void Dispose()
        {
            _excelPackage.Dispose();
        }
    }
}

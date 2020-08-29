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
            _excelPackage.Workbook.Properties.Title = "Huishoudboekje";
            _excelPackage.Workbook.Properties.Subject = "Export";
            _excelPackage.Workbook.Properties.Created = DateTime.Now;
            ExcelWorksheet worksheet = _excelPackage.Workbook.Worksheets.Add("Huishoudboek");

            var householdPostWorksheet = new HouseholdPostWorksheet(worksheet);
            householdPostWorksheet.Write(householdBook);
            await _excelPackage.SaveAsync();
        }

        public void Dispose()
        {
            _excelPackage.Dispose();
        }
    }
}

using OfficeOpenXml;
using System.Drawing;

namespace Bankreader.FileSystem.Excel.Extensions
{
    public static class ExcelWorksheetExtensions
    {
        public static ExcelRange GetCell(this ExcelWorksheet excelWorksheet, Point point)
        {
            return excelWorksheet.Cells[point.Y, point.X];
        }
    }
}

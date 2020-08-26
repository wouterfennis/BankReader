using OfficeOpenXml;
using System.Drawing;

namespace BankReader.Data.Excel.Extensions
{
    public static class ExcelRangeExtensions
    {
        public static void SetBackgroundColor(this ExcelRange excelRange, Color color)
        {
            excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(color);
        }

        public static void SetFontColor(this ExcelRange excelRange, Color color)
        {
            excelRange.Style.Font.Color.SetColor(color);
        }

        public static void ConvertToEuro(this ExcelRange excelRange)
        {
            excelRange.Style.Numberformat.Format = "€#,##0.00";
            excelRange.Value = 0;
        }

        public static void SetSumFormula(this ExcelRange excelRange, ExcelRange startRange, ExcelRange endRange)
        {
            excelRange.Formula = "=SUM(" + startRange.Address + ":" + endRange.Address + ")";
        }

        public static void SetAverageFormula(this ExcelRange excelRange, ExcelRange startRange, ExcelRange endRange)
        {
            excelRange.Formula = "=AVERAGE(" + startRange.Address + ":" + endRange.Address + ")";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;

namespace BankReader.Data.Excel.Extensions
{
    public static class ExcelRangeExtensions
    {
        public static void SetBackgroundColor(this ExcelRange excelRange, int red, int blue, int green)
        {
            excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            excelRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(red, blue, green));
        }

        public static void SetSumFormula(this ExcelRange excelRange, ExcelRange startRange, ExcelRange endRange)
        {
            excelRange.Formula = "=SUM(" + startRange.Address + ":" + endRange.Address + ")";
        }

        public static void SetAverageFormula(this ExcelRange excelRange, ExcelRange startRange, ExcelRange endRange)
        {
            excelRange.Formula = "=AVERAGE(" + startRange.Address + ":" + endRange.Address + ")";
        }

        public static void ConvertToEuro(this ExcelRange excelRange)
        {
            excelRange.Style.Numberformat.Format = "€#,##0.00";
            excelRange.Value = 0;
        }
    }
}

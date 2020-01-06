using OfficeOpenXml;
using System.Drawing;

namespace BankReader.Data.Excel
{
    class ExcelWorksheetWriter : IWorksheetWriter
    {
        public ExcelWorksheet ExcelWorksheet { get; }
        public Point CurrentPosition { get; private set; }

        private ExcelRange CurrentCell
        {
            get
            {
                return ExcelWorksheet.Cells[CurrentPosition.X, CurrentPosition.Y];
            }
        }

        public ExcelWorksheetWriter(ExcelWorksheet excelWorksheet, Point startPoint)
        {
            ExcelWorksheet = excelWorksheet;
            CurrentPosition = startPoint;
        }

        public void SetCurrentPosition(Point newPosition)
        {
            CurrentPosition = newPosition;
        }

        public void MoveDown()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
        }

        public void MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
        }

        public void MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
        }

        public void MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
        }

        public void SetColor(Color newColor)
        {
            CurrentCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            CurrentCell.Style.Fill.BackgroundColor.SetColor(newColor);
        }

        public void SetData(decimal value)
        {
            CurrentCell.Style.Numberformat.Format = "€#,##0.00";
            CurrentCell.Value = value;
        }

        public void SetData(string value)
        {
            CurrentCell.Value = value;
        }

    }
}

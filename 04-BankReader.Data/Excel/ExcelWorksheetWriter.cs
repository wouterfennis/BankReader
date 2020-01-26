using BankReader.Data.Excel.Extensions;
using OfficeOpenXml;
using System.Drawing;

namespace BankReader.Data.Excel
{
    public class ExcelWorksheetWriter : IWorksheetWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private Color _currentColor;

        public Point CurrentPosition { get; set; }

        public ExcelRange CurrentCell { get => _excelWorksheet.GetCell(CurrentPosition); }

        public ExcelWorksheetWriter(ExcelWorksheet excelWorksheet)
        {
            _excelWorksheet = excelWorksheet;
        }

        public void MoveDown()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
        }

        public void MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
        }

        public void MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
        }

        public void MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
        }

        public void SetColor(Color color)
        {
            _currentColor = color;
        }

        public void Write(decimal value)
        {
            CurrentCell.ConvertToEuro();
            Write(value);
        }

        public void Write(string value)
        {
            Write(value);
        }

        private void Write(object value)
        {
            CurrentCell.SetBackgroundColor(_currentColor);
            CurrentCell.Value = value;
        }

        public void PlaceFormula(Point startPosition, Point endPosition, Point resultPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(resultPosition);

            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;
        }
    }
}

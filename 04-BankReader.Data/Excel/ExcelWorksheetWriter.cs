using BankReader.Data.Excel.Extensions;
using OfficeOpenXml;
using System;
using System.Drawing;

namespace BankReader.Data.Excel
{
    public class ExcelWorksheetWriter : IWorksheetWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private Color _currentColor;
        private readonly int DefaultXPosition = 1;
        private readonly int DefaultYPosition = 1;

        public Point CurrentPosition { get; set; }

        public ExcelRange CurrentCell { get => _excelWorksheet.GetCell(CurrentPosition); }

        public ExcelWorksheetWriter(ExcelWorksheet excelWorksheet)
        {
            _excelWorksheet = excelWorksheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
            _currentColor = Color.White;
        }

        public IWorksheetWriter MoveDown()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            return this;
        }

        public IWorksheetWriter MoveUp()
        {
            CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            return this;
        }

        public IWorksheetWriter MoveLeft()
        {
            CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            return this;
        }

        public IWorksheetWriter MoveRight()
        {
            CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            return this;
        }

        public IWorksheetWriter SetColor(Color color)
        {
            _currentColor = color;
            return this;
        }

        public IWorksheetWriter Write(decimal value)
        {
            CurrentCell.ConvertToEuro();
            CurrentCell.SetBackgroundColor(_currentColor);
            CurrentCell.Value = value;
            return this;
        }

        public IWorksheetWriter Write(string value)
        {
            CurrentCell.SetBackgroundColor(_currentColor);
            CurrentCell.Value = value;
            return this;
        }

        public IWorksheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        public IWorksheetWriter PlaceFormula(Point startPosition, Point endPosition, Point resultPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(resultPosition);

            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;
            return this;
        }
    }
}

using Bankreader.Application.Interfaces;
using BankReader.Data.Excel.Extensions;
using OfficeOpenXml;
using System;
using System.Drawing;

namespace BankReader.Data.Excel
{
    public class ExcelWorksheetWriter : IWorksheetWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private Color _currentBackgroundColor;
        private Color _currentFontColor;
        private readonly int DefaultXPosition = 1;
        private readonly int DefaultYPosition = 1;

        public Point CurrentPosition { get; set; }

        public ExcelRange CurrentCell { get => _excelWorksheet.GetCell(CurrentPosition); }

        public ExcelWorksheetWriter(ExcelWorksheet excelWorksheet)
        {
            _excelWorksheet = excelWorksheet;
            CurrentPosition = new Point(DefaultXPosition, DefaultYPosition);
            _currentBackgroundColor = Color.White;
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

        public IWorksheetWriter SetBackgroundColor(Color color)
        {
            _currentBackgroundColor = color;
            return this;
        }

        public IWorksheetWriter SetFontColor(Color color)
        {
            _currentFontColor = color;
            return this;
        }

        public IWorksheetWriter Write(decimal value)
        {
            CurrentCell.ConvertToEuro();
            CurrentCell.SetBackgroundColor(_currentBackgroundColor);
            CurrentCell.SetFontColor(_currentFontColor);
            CurrentCell.Value = value;
            return this;
        }

        public IWorksheetWriter Write(string value)
        {
            CurrentCell.SetBackgroundColor(_currentBackgroundColor);
            CurrentCell.Value = value;
            return this;
        }

        public IWorksheetWriter NewLine()
        {
            CurrentPosition = new Point(DefaultXPosition, CurrentPosition.Y + 1);
            return this;
        }

        public IWorksheetWriter PlaceFormula(Point startPosition, Point endPosition, FormulaType formulaType)
        {
            var startCell = _excelWorksheet.GetCell(startPosition);
            var endCell = _excelWorksheet.GetCell(endPosition);
            var resultCell = _excelWorksheet.GetCell(CurrentPosition);

            Write(0);
            var formula = $"={formulaType}({startCell.Address}:{endCell.Address})";
            resultCell.Formula = formula;
            return this;
        }
    }
}

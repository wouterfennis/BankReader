using BankReader.Data.Excel.Extensions;
using OfficeOpenXml;
using System;

namespace BankReader.Data.Excel
{
    public class ExcelWriter
    {
        private readonly ExcelWorksheet excelWorksheet;
        private int xPosition;
        private int yPosition;
        private ExcelRange CurrentCell
        {
            get
            {
                return excelWorksheet.Cells[yPosition, xPosition];
            }
        }

        public ExcelWriter(ExcelWorksheet excelWorksheet, int xPosition, int yPosition)
        {
            ValidateWorkSheet(excelWorksheet);
            ValidateXPosition(xPosition);
            ValidateYPosition(yPosition);
            this.excelWorksheet = excelWorksheet;
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public ExcelWriter Write(string content)
        {
            CurrentCell.Value = content;
            return this;
        }

        public ExcelWriter MoveRight()
        {
            xPosition++;
            return this;
        }

        public ExcelWriter LeftRight()
        {
            xPosition++;
            return this;
        }

        public ExcelWriter MoveDown()
        {
            yPosition++;
            return this;
        }

        public ExcelWriter NewLine()
        {
            yPosition++;
            xPosition = 1;
            return this;
        }

        public ExcelWriter SetBackgroundColor(int red, int green, int blue)
        {
            CurrentCell.SetBackgroundColor(red, green, blue);
            return this;
        }

        private void ValidateWorkSheet(ExcelWorksheet excelWorksheet)
        {
            if (excelWorksheet == null)
            {
                throw new ArgumentNullException($"{nameof(ExcelWorksheet)} is null");
            }
        }

        private void ValidateXPosition(int positionValue)
        {
            if (positionValue < 0)
            {
                throw new ArgumentException($"X position is lower than zero");
            }
        }

        private void ValidateYPosition(int positionValue)
        {
            if (positionValue < 0)
            {
                throw new ArgumentException($"Y position is lower than zero");
            }
        }
    }
}

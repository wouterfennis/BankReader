using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BankReader.Data.Excel
{
    public interface IWorksheetWriter
    {
        void MoveUp();

        void MoveDown();

        void MoveLeft();

        void MoveRight();

        void SetColor(Color color);

        void Write(decimal value);

        void Write(string value);

        void PlaceFormula(Point startPosition, Point endPosition, Point resultPosition, FormulaType formulaType);
    }
}

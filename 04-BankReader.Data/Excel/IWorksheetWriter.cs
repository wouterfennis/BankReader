using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BankReader.Data.Excel
{
    public interface IWorksheetWriter
    {
        Point CurrentPosition { get; set; }

        IWorksheetWriter MoveUp();

        IWorksheetWriter MoveDown();

        IWorksheetWriter MoveLeft();

        IWorksheetWriter MoveRight();

        IWorksheetWriter SetColor(Color color);

        IWorksheetWriter Write(decimal value);

        IWorksheetWriter Write(string value);

        IWorksheetWriter PlaceFormula(Point startPosition, Point endPosition, Point resultPosition, FormulaType formulaType);
    }
}

using Bankreader.Application.Models;
using System.Drawing;

namespace Bankreader.Application.Interfaces
{
    public interface IWorksheetWriter
    {
        Point CurrentPosition { get; set; }

        IWorksheetWriter MoveUp();

        IWorksheetWriter MoveDown();

        IWorksheetWriter MoveLeft();

        IWorksheetWriter MoveRight();

        IWorksheetWriter NewLine();

        IWorksheetWriter SetBackgroundColor(Color color);

        IWorksheetWriter SetFontColor(Color color);

        IWorksheetWriter Write(decimal value);

        IWorksheetWriter Write(string value);

        IWorksheetWriter PlaceFormula(Point startPosition, Point endPosition, FormulaType formulaType);
    }
}

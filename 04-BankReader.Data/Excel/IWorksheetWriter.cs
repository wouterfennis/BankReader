using System.Drawing;

namespace BankReader.Data.Excel
{
    public interface IWorksheetWriter
    {
        Point CurrentPosition { get; set; }

        IWorksheetWriter MoveUp();

        IWorksheetWriter MoveDown();

        IWorksheetWriter MoveLeft();

        IWorksheetWriter MoveRight();

        IWorksheetWriter NewLine();

        IWorksheetWriter SetColor(Color color);

        IWorksheetWriter Write(decimal value);

        IWorksheetWriter Write(string value);

        IWorksheetWriter PlaceFormula(Point startPosition, Point endPosition, FormulaType formulaType);
    }
}

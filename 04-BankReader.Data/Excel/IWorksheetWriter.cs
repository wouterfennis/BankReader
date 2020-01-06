using System.Drawing;

namespace BankReader.Data.Excel
{
    public interface IWorksheetWriter
    {
        void SetCurrentPosition(Point newPosition);

        void MoveRight();

        void MoveLeft();

        void MoveUp();

        void MoveDown();

        void SetColor(Color newColor);

        void SetData(decimal value);

        void SetData(string value);
    }
}

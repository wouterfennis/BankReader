using BankReader.Implementation.Models;

namespace BankReader.Implementation.Wrappers
{
    public interface IConsoleScreen
    {
        string ReadLine();

        void WriteLine(string output);

        string AskForPath(string subject);

        InputType AskForInputType(string subject);
    }
}

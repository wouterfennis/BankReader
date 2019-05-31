using BankReader.Implementation.Models;
using System;
using System.Text;

namespace BankReader.Implementation.Wrappers
{
    public class ConsoleScreen : IConsoleScreen
    {
        private readonly IFileWrapper _fileWrapper;

        public ConsoleScreen(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public string AskForPath(string subject)
        {
            Console.WriteLine($"Fill in the path for the {subject} file:");
            var filePath = Console.ReadLine();
            if (!_fileWrapper.Exists(filePath))
            {
                Console.WriteLine("The path you entered was incorrect!");
                return AskForPath(subject);
            }

            return filePath;
        }

        public InputType AskForInputType(string subject)
        {
            Console.WriteLine($"Which type is your {subject} input? Types to choose are:");
            Console.WriteLine(GetInputTypesString());
            var input = Console.ReadLine();

            if (!Enum.TryParse(input, out InputType result))
            {
                Console.WriteLine($"'{input}'is not a valid input type.");
                return AskForInputType(subject);
            }

            return result;
        }

        private static string GetInputTypesString()
        {
            var stringBuilder = new StringBuilder();
            foreach (string inputType in Enum.GetNames(typeof(InputType)))
            {
                stringBuilder.Append(inputType + "|");
            }
            return stringBuilder.ToString();
        }
    }
}
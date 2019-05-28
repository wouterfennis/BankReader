using System;
using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Data.Excel;
using BankReader.Implementation;
using BankReader.Implementation.Wrappers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BankReader.Data.Json;
using BankReader.Implementation.Models;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly IConsoleWrapper _consoleWrapper;
        private readonly IJsonReader _jsonReader;
        private readonly ITransactionReader _transactionReader;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Application(IConsoleWrapper consoleWrapper, 
            IJsonReader jsonReader, 
            ITransactionReader transactionReader, 
            ICategoryService categoryService, 
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _consoleWrapper = consoleWrapper;
            _jsonReader = jsonReader;
            _transactionReader = transactionReader;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var inputType = AskForInputType();
            var filePath = AskForPath();

            List<Transaction> transactions = _transactionReader.ReadCsv(filePath).ToList();
            _consoleWrapper.WriteLine($"There are {transactions.Count} transactions in this CSV");

            var huishoudTransacties = _categoryService.Categorise(transactions);

            _housekeepingBookWriter.Write(huishoudTransacties);
        }

        private string AskForPath()
        {
            _consoleWrapper.WriteLine("Fill in the path to the CSV file:");
            var filePath = _consoleWrapper.ReadLine();
            if (!File.Exists(filePath))
            {
                _consoleWrapper.WriteLine("The path you entered was incorrect!");
                return AskForPath();
            }

            return filePath;
        }

        private InputType AskForInputType()
        {
            _consoleWrapper.WriteLine("Which type is your input? Types to choose are:");
            _consoleWrapper.WriteLine(GetInputTypesString());
            var input = _consoleWrapper.ReadLine();
            if (Enum.TryParse<InputType>(input, out InputType result))
            {
                _consoleWrapper.WriteLine($"'{input}'is not a valid input type.");
                return AskForInputType();
            }

            return result;
        }

        public static string GetInputTypesString()
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

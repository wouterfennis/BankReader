using Bankreader.Domain.Models;
using System.Linq;

namespace Bankreader.ConsoleHost
{
    internal class ArgumentOptions
    {
        public FilePath TransactionsLocation { get; }
        public FilePath CategoryRulesLocation { get; }
        public FilePath WorkbookLocation { get; }

        public ArgumentOptions(string[] arguments)
        {
            TransactionsLocation = arguments.ElementAt(0);
            CategoryRulesLocation = arguments.ElementAt(1);
            WorkbookLocation = arguments.ElementAt(2);
        }
    }
}

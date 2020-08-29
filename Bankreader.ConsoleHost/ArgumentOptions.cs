using Bankreader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankreader.ConsoleHost
{
    internal class ArgumentOptions
    {
        public FilePath TransactionsLocation { get; }
        public FilePath CategoryRulesLocation { get; }
        public FilePath WorkbookLocation { get; }

        private readonly byte _numberOfArguments = 3;
        private readonly byte _transactionLocationIndex = 0;
        private readonly byte _categoryRulesLocationIndex = 1;
        private readonly byte _workbookLocationIndex = 2;

        public ArgumentOptions(IEnumerable<string> arguments)
        {
            if (arguments.Count() != _numberOfArguments)
            {
                throw new ArgumentException($"There aren't exactly {_numberOfArguments} arguments passed", nameof(arguments));
            }

            TransactionsLocation = arguments.ElementAt(_transactionLocationIndex);
            CategoryRulesLocation = arguments.ElementAt(_categoryRulesLocationIndex);
            WorkbookLocation = arguments.ElementAt(_workbookLocationIndex);
        }
    }
}

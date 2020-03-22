using BankReader.Data.Excel;
using BankReader.Implementation.Services;
using BankReader.Data.Csv;
using BankReader.Data.Json;
using Autofac;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly ICsvTransactionReader _csvTransactionReader;
        private readonly IJsonRuleReader _jsonRuleReader;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;
        private readonly IContainer container;

        public Application(ICsvTransactionReader CsvTransactionReader, 
            IJsonRuleReader jsonRuleReader,
            ICategoryService categoryService,
            IHousekeepingBookWriter housekeepingBookWriter,
            IContainer container)
        {
            _csvTransactionReader = CsvTransactionReader;
            _jsonRuleReader = jsonRuleReader;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
            this.container = container;
        }

        public void Run()
        {
            container.
            var filePathTransactions = @"C:\Git\BankReader\test.csv";
            var transactions = _csvTransactionReader.ReadTransactions(filePathTransactions);

            var filePathRules = @"C:\Git\BankReader\test.json";
            var rules = _jsonRuleReader.ReadRules(filePathRules);

            var houseHoldPosts = _categoryService.Categorise(rules, transactions);

            _housekeepingBookWriter.Write(houseHoldPosts);
        }
    }
}

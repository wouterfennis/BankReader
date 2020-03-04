using BankReader.Data.Excel;
using BankReader.Implementation.Services;
using BankReader.Data.Csv;
using BankReader.Data.Json;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly ICsvTransactionReader _csvTransactionReader;
        private readonly IJsonRuleReader _jsonRuleReader;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Application(ICsvTransactionReader CsvTransactionReader, 
            IJsonRuleReader jsonRuleReader,
            ICategoryService categoryService,
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _csvTransactionReader = CsvTransactionReader;
            _jsonRuleReader = jsonRuleReader;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var filePathTransactions = @"C:\Git\BankReader\test.csv";
            var transactions = _csvTransactionReader.ReadTransactions(filePathTransactions);

            var filePathRules = @"C:\Git\BankReader\test.json";
            var rules = _jsonRuleReader.ReadRules(filePathRules);

            var houseHoldPosts = _categoryService.Categorise(rules, transactions);

            _housekeepingBookWriter.Write(houseHoldPosts);
        }
    }
}

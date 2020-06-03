using BankReader.Data.Csv;
using BankReader.Data.Excel;
using BankReader.Data.Json;
using BankReader.Implementation.Services;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly ITransactionProvider _transactionProvider;
        private readonly ICategoryRuleProvider _ruleProvider;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Application(ITransactionProvider transactionProvider,
            ICategoryRuleProvider ruleProvider,
            ICategoryService categoryService,
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _transactionProvider = transactionProvider;
            _ruleProvider = ruleProvider;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var houseHoldPosts = _categoryService.Categorise();

            _housekeepingBookWriter.Write(houseHoldPosts);
        }
    }
}

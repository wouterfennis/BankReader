using BankReader.Data.Excel;
using BankReader.Implementation.Services;
using BankReader.Data.Csv;
using BankReader.Data.Json;
using Autofac;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly ITransactionProvider _transactionProvider;
        private readonly ICategoryRuleProvider _ruleProvider;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;
        private readonly IContainer container;

        public Application(ITransactionProvider transactionProvider, 
            ICategoryRuleProvider ruleProvider,
            ICategoryService categoryService,
            IHousekeepingBookWriter housekeepingBookWriter,
            IContainer container)
        {
            _transactionProvider = transactionProvider;
            _ruleProvider = ruleProvider;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
            this.container = container;
        }

        public void Run()
        {
            var houseHoldPosts = _categoryService.Categorise();

            _housekeepingBookWriter.Write(houseHoldPosts);
        }
    }
}

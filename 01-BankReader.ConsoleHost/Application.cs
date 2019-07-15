using Autofac.Features.Indexed;
using BankReader.Data.Excel;
using BankReader.Implementation.Models;
using BankReader.Implementation.Services;
using BankReader.Implementation.Wrappers;
using BankReader.Implementation.Rules;
using BankReader.Implementation.Transactions;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly IConsoleScreen _consoleScreen;
        private readonly ICategoryService _categoryService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;
        private readonly IIndex<InputType, IRuleService> _ruleServices;
        private readonly IIndex<InputType, ITransactionService> _transactionServices;

        public Application(IConsoleScreen consoleScreen,
            IIndex<InputType, IRuleService> ruleServices,
            IIndex<InputType, ITransactionService> transactionServices,
            ICategoryService categoryService,
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _transactionServices = transactionServices;
            _consoleScreen = consoleScreen;
            _ruleServices = ruleServices;
            _categoryService = categoryService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var ruleInputType = _consoleScreen.AskForInputType("rules");
            var ruleService = _ruleServices[ruleInputType];
            var rules =  ruleService.RetrieveRules();

            var transactionInputType = _consoleScreen.AskForInputType("transactions");
            var transactionService = _transactionServices[transactionInputType];
            var transactions = transactionService.RetrieveTransactions();

            var houseHoldPosts = _categoryService.Categorise(rules, transactions);

            _housekeepingBookWriter.Write(houseHoldPosts);
        }
    }
}

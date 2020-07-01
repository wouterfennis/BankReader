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
        private readonly IHouseholdService _householdService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Application(ITransactionProvider transactionProvider,
            ICategoryRuleProvider ruleProvider,
            IHouseholdService householdService,
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _transactionProvider = transactionProvider;
            _ruleProvider = ruleProvider;
            _householdService = householdService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var householdBook = _householdService.CreateHouseholdBook();

            _housekeepingBookWriter.Write(householdBook);
        }
    }
}

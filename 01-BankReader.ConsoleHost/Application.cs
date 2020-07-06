using BankReader.Data.Excel;
using BankReader.Implementation.Services;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly IHouseholdService _householdService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Application(IHouseholdService householdService,
            IHousekeepingBookWriter housekeepingBookWriter)
        {
            _householdService = householdService;
            _housekeepingBookWriter = housekeepingBookWriter;
        }

        public void Run()
        {
            var householdBook = _householdService.CreateHouseholdBook();

            _housekeepingBookWriter.WriteAsync(householdBook);
        }
    }
}

using BankReader.ConsoleHost.Interfaces;

namespace BankReader.ConsoleHost
{
    public class Application
    {
        private readonly IHouseholdService _householdService;
        private readonly IHouseholdBookWriter _housekeepingBookWriter;

        public Application(IHouseholdService householdService,
            IHouseholdBookWriter housekeepingBookWriter)
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

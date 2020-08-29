using Bankreader.Application.Interfaces;

namespace Bankreader.Application
{
    public class Controller
    {
        private readonly IHouseholdService _householdService;
        private readonly IHousekeepingBookWriter _housekeepingBookWriter;

        public Controller(IHouseholdService householdService,
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

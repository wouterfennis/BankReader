using BankReader.Data.Models;

namespace BankReader.Data.Excel
{
    public interface IHousekeepingBookWriter
    {
        void Write(HouseholdBook householdBook);
    }
}

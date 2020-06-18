using BankReader.Data.Models;
using System.Collections.Generic;

namespace BankReader.Data.Excel
{
    public interface IHousekeepingBookWriter
    {
        void Write(HouseholdBook householdBook);
    }
}

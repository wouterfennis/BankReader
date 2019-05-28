using BankReader.Implementation.Models;
using System.Collections.Generic;

namespace BankReader.Data.Excel
{
    public interface IHousekeepingBookWriter
    {
        void Write(IEnumerable<HouseholdPost> houseHoldPosts);
    }
}

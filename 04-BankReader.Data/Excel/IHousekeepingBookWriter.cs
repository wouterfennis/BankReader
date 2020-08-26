using BankReader.Shared.Models;
using System;
using System.Threading.Tasks;

namespace BankReader.Data.Excel
{
    public interface IHousekeepingBookWriter : IDisposable
    {
        Task WriteAsync(HouseholdBook householdBook);
    }
}

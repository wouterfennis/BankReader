using BankReader.Shared.Models;
using System;
using System.Threading.Tasks;

namespace BankReader.ConsoleHost.Interfaces
{
    public interface IHouseholdBookWriter : IDisposable
    {
        Task WriteAsync(HouseholdBook householdBook);
    }
}

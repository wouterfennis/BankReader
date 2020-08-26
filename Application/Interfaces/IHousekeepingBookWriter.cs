using Bankreader.Application.Models;
using BankReader.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Bankreader.Application.Interfaces
{
    public interface IHousekeepingBookWriter : IDisposable
    {
        Task WriteAsync(HouseholdBook householdBook);
    }
}

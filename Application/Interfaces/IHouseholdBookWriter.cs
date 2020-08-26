using BankReader.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Bankreader.Application.Interfaces
{
    public interface IHouseholdBookWriter : IDisposable
    {
        Task WriteAsync(HouseholdBook householdBook);
    }
}

using Bankreader.Application.Models;
using BankReader.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Bankreader.FileSystem.Excel
{
    public interface IHousekeepingBookWriter : IDisposable
    {
        Task WriteAsync(HouseholdBook householdBook);
    }
}

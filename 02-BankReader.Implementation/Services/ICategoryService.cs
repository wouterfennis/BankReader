using System.Collections.Generic;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Implementation.Models;

namespace BankReader.Implementation.Services
{
    public interface ICategoryService
    {
        IEnumerable<HouseholdPost> Categorise(IEnumerable<CategoryRule> rules, IEnumerable<Banktransaction> transactions);
    }
}

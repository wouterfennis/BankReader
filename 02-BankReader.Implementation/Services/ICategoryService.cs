using System.Collections.Generic;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Implementation.Models;

namespace BankReader.Implementation.Services
{
    public interface ICategoryService
    {
        IEnumerable<HouseholdPost> Categorise(IList<CategoryRule> rules, IList<Transaction> transactions);
    }
}

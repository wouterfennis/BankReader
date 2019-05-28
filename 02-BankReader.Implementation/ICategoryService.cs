using BankReader.Data.Csv.Models;
using BankReader.Implementation.Models;
using System.Collections.Generic;

namespace BankReader.Implementation
{
    public interface ICategoryService
    {
        IEnumerable<HouseholdPost> Categorise(IEnumerable<Transaction> transactions);
    }
}

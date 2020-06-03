using BankReader.Data.Models;
using System.Collections.Generic;

namespace BankReader.Implementation.Models
{
    public class HouseholdPost
    {
        public Category Category { get; }
        public IList<HouseholdTransaction> Transactions { get; }

        public HouseholdPost(Category category)
        {
            Category = category;
            Transactions = new List<HouseholdTransaction>();
        }
    }
}
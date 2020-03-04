using System;
using System.Collections.Generic;
using System.Text;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Models;

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
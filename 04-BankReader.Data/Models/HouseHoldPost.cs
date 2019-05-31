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
            public Category Category { get; set; }
            public List<HouseholdTransaction> Transactions { get; set; }
        }
}

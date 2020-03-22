using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Models;

namespace BankReader.Data.Models
{
    public class CategoryRule
    {
        public string[] Descriptions { get; set; }
        public Category Category { get; set; }

        public bool Validate(Banktransaction transaction)
        {
            return Descriptions.Any(description => transaction.Description.ToUpper().Contains(description.ToUpper()));
        }
    }
}

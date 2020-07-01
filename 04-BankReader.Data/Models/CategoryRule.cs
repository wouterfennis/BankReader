using System.Linq;

namespace BankReader.Data.Models
{
    public class CategoryRule
    {
        public string[] DescriptionMatches { get; set; }
        public Category Category { get; set; }

        public bool Validate(string description)
        {
            return DescriptionMatches.Any(descriptionMatch => description.ToUpper().Contains(descriptionMatch.ToUpper()));
        }
    }
}

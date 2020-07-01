using BankReader.Data.Json;
using BankReader.Data.Models;

namespace BankReader.Implementation.Services
{
    public class TransactionCategorizer : ITransactionCategorizer
    {
        private readonly ICategoryRuleProvider _categoryRuleProvider;

        public TransactionCategorizer(ICategoryRuleProvider categoryRuleProvider)
        {
            _categoryRuleProvider = categoryRuleProvider;
        }

        public Category DetermineCategory(string description)
        {
            var rules = _categoryRuleProvider.ProvideRules();
            foreach (var categoryRule in rules)
            {
                if (categoryRule.Validate(description))
                {
                    return categoryRule.Category;
                }
            }
            return Category.Unknown;
        }
    }
}

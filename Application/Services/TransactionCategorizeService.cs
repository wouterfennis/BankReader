using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;

namespace Bankreader.Application.Services
{
    public class TransactionCategorizeService : ITransactionCategorizer
    {
        private readonly ICategoryRuleProvider _categoryRuleProvider;

        public TransactionCategorizeService(ICategoryRuleProvider categoryRuleProvider)
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

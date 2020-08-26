using BankReader.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BankReader.Data.UnitTests.TestdataBuilders
{
    public class JsonCategoryRulesBuilder
    {
        private List<CategoryRule> CategoryRules { get; set; }

        public JsonCategoryRulesBuilder()
        {
            CategoryRules = new List<CategoryRule>();
        }

        public JsonCategoryRulesBuilder AddTaxRule()
        {
            CategoryRules.Add(new CategoryRule
            {
                Category = Category.Tax,
                DescriptionMatches = new[] { "belasting" }
            });
            return this;
        }

        public string Build()
        {
            return JsonConvert.SerializeObject(CategoryRules);
        }
    }
}

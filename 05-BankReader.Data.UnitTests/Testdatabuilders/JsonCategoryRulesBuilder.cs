using BankReader.Data.Models;
using BankReader.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

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
                Descriptions = new[] { "belasting" }
            });
            return this;
        }

        public string Build()
        {
            return JsonConvert.SerializeObject(CategoryRules);
        }
    }
}

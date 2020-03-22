using System.Collections.Generic;
using BankReader.Data.Models;

namespace BankReader.Data.Json
{
    public interface ICategoryRuleProvider
    {
        IList<CategoryRule> ProvideRules();
    }
}
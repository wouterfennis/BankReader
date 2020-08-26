using BankReader.Shared.Models;
using System.Collections.Generic;

namespace BankReader.Data.Json
{
    public interface ICategoryRuleProvider
    {
        IList<CategoryRule> ProvideRules();
    }
}
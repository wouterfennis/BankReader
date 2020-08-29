using Bankreader.Application.Models;
using BankReader.Shared.Models;
using System.Collections.Generic;

namespace Bankreader.Application.Interfaces
{
    public interface ICategoryRuleProvider
    {
        IList<CategoryRule> ProvideRules();
    }
}
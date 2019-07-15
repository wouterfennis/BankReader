using System.Collections.Generic;
using BankReader.Data.Models;

namespace BankReader.Implementation.Rules
{
    public interface IRuleService
    {
        IList<CategoryRule> RetrieveRules();
    }
}
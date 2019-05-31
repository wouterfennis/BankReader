using System.Collections.Generic;
using BankReader.Data.Models;

namespace BankReader.Implementation.Services
{
    public interface IRuleService
    {
        IList<CategoryRule> RetrieveRules();
    }
}
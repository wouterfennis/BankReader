using System.Collections.Generic;
using BankReader.Data.Models;

namespace BankReader.Data.Json
{
    public interface IJsonRuleReader
    {
        IList<CategoryRule> ReadRules(string path);
    }
}
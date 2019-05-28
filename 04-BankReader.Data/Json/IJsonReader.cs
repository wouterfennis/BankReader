using System.Collections.Generic;
using BankReader.Data.Models;

namespace BankReader.Data.Json
{
    public interface IJsonReader
    {
        IEnumerable<CategoryRule> ReadRules(string path);
    }
}
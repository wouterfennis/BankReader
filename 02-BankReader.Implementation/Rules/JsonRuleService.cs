using System.Collections.Generic;
using BankReader.Data.Json;
using BankReader.Data.Models;
using BankReader.Implementation.Wrappers;

namespace BankReader.Implementation.Rules
{
    public class JsonRuleService : IRuleService
    {
        private readonly IJsonReader _jsonReader;
        private readonly IConsoleScreen _consoleScreen;

        public JsonRuleService(IConsoleScreen consoleScreen, IJsonReader jsonReader)
        {
            _consoleScreen = consoleScreen;
            _jsonReader = jsonReader;
        }

        public IList<CategoryRule> RetrieveRules()
        {
            var path = _consoleScreen.AskForPath("rule json");
            return _jsonReader.ReadRules(path);
        }
    }
}

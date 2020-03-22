using System.Collections.Generic;
using System.IO;
using BankReader.Data.Models;
using BankReader.Data.Providers;
using BankReader.Data.Utilities;
using Newtonsoft.Json;

namespace BankReader.Data.Json
{
    public class JsonRuleReader : IJsonRuleReader
    {
        private readonly ICategoryRulesLocationProvider _categoryRulesLocationProvider;
        private readonly ITextStreamFactory _textStreamFactory;

        public JsonRuleReader(ICategoryRulesLocationProvider categoryRulesLocationProvider, ITextStreamFactory textStreamFactory)
        {
            _categoryRulesLocationProvider = categoryRulesLocationProvider;
            _textStreamFactory = textStreamFactory;
        }

        public IList<CategoryRule> ReadRules()
        {
            var path = _categoryRulesLocationProvider.GetCategoryRulesLocation();
            using (TextReader textReader = _textStreamFactory.Create(path))
            {
                var json = textReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IList<CategoryRule>>(json);
            }
        }
    }
}

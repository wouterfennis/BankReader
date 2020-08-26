using System.Collections.Generic;
using System.IO;
using BankReader.Shared.Models;
using BankReader.Data.Providers;
using BankReader.Data.Utilities;
using Newtonsoft.Json;

namespace BankReader.Data.Json
{
    public class JsonCategoryRuleProvider : ICategoryRuleProvider
    {
        private readonly IFileLocationProvider _fileLocationProvider;
        private readonly ITextStreamFactory _textStreamFactory;

        public JsonCategoryRuleProvider(IFileLocationProvider categoryRulesLocationProvider, ITextStreamFactory textStreamFactory)
        {
            _fileLocationProvider = categoryRulesLocationProvider;
            _textStreamFactory = textStreamFactory;
        }

        public IList<CategoryRule> ProvideRules()
        {
            var path = _fileLocationProvider.GetCategoryRulesLocation();
            using (TextReader textReader = _textStreamFactory.Create(path))
            {
                var json = textReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IList<CategoryRule>>(json);
            }
        }
    }
}

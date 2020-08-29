using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.Infrastructure.Files;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bankreader.FileSystem.Json
{
    public class JsonCategoryRuleReader : ICategoryRuleProvider
    {
        private readonly Application.Interfaces.IFileLocationProvider _fileLocationProvider;
        private readonly ITextStreamFactory _textStreamFactory;

        public JsonCategoryRuleReader(Application.Interfaces.IFileLocationProvider categoryRulesLocationProvider, ITextStreamFactory textStreamFactory)
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

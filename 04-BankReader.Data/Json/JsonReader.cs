using System.Collections.Generic;
using System.IO;
using BankReader.Data.Models;
using BankReader.Data.Utilities;
using Newtonsoft.Json;

namespace BankReader.Data.Json
{
    public class JsonReader : IJsonReader
    {
        private readonly ITextStreamFactory _textStreamFactory;

        public JsonReader(ITextStreamFactory textStreamFactory)
        {
            _textStreamFactory = textStreamFactory;
        }

        public IList<CategoryRule> ReadRules(string path)
        {
            using (TextReader textReader = _textStreamFactory.Create(path))
            {
                var json = textReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IList<CategoryRule>>(json);
            }
        }
    }
}

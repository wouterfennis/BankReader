using System.IO;

namespace BankReader.Data.Utilities
{
    public class TextStreamFactory : ITextStreamFactory
    {
        public TextReader Create(string path)
        {
            return new StreamReader(path);
        }
    }
}

using System.IO;

namespace BankReader.Data.Utilities
{
    public class TextStreamFactory : ITextStreamFactory
    {
        public TextReader Create(IFileInfoWrapper path)
        {
            return new StreamReader(path.OpenRead());
        }
    }
}

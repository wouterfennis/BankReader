using Bankreader.Application.Interfaces;
using Bankreader.Infrastructure.Files;
using System.IO;

namespace Bankreader.FileSystem.File
{
    public class TextStreamFactory : ITextStreamFactory
    {
        public TextReader Create(IFileInfoWrapper path)
        {
            return new StreamReader(path.OpenRead());
        }
    }
}

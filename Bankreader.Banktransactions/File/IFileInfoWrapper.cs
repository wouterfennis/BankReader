using System.IO;

namespace BankReader.Data.Utilities
{
    public interface IFileInfoWrapper
    {
        Stream OpenRead();

        FileInfo ToFileInfo();
    }
}

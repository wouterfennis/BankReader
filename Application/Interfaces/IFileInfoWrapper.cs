using System.IO;

namespace Bankreader.Application.Interfaces
{
    public interface IFileInfoWrapper
    {
        Stream OpenRead();

        FileInfo ToFileInfo();
    }
}

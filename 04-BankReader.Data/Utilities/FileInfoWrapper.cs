using System.IO;

namespace BankReader.Data.Utilities
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly string _filePath;

        public FileInfoWrapper(string filePath)
        {
            _filePath = filePath;
        }

        public Stream OpenRead()
        {
            var fileInfo = new FileInfo(_filePath);
            return fileInfo.OpenRead();
        }

        public FileInfo ToFileInfo()
        {
            return new FileInfo(_filePath);
        }
    }
}

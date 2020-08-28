using Bankreader.Application.Interfaces;
using Bankreader.Domain.Models;
using System.IO;

namespace Bankreader.FileSystem.File
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly FilePath _filePath;

        public FileInfoWrapper(FilePath filePath)
        {
            _filePath = filePath;
        }

        public Stream OpenRead()
        {
            var fileInfo = new FileInfo(_filePath.Value);
            return fileInfo.OpenRead();
        }

        public FileInfo ToFileInfo()
        {
            return new FileInfo(_filePath.Value);
        }
    }
}

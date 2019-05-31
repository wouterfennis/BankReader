using System.IO;

namespace BankReader.Implementation.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}

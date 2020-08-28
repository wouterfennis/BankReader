using Bankreader.Application.Interfaces;
using Bankreader.Domain.Models;

namespace Bankreader.FileSystem.File
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly FilePath _categoryRulesLocation;
        private readonly FilePath _transactionsLocation;
        private readonly FilePath _workbookLocation;

        public FileLocationProvider(FilePath categoryRulesLocation, FilePath transactionsLocation, FilePath workbookLocation)
        {
            _categoryRulesLocation = categoryRulesLocation;
            _transactionsLocation = transactionsLocation;
            _workbookLocation = workbookLocation;
        }

        public IFileInfoWrapper GetCategoryRulesLocation()
        {
            return new FileInfoWrapper(_categoryRulesLocation);
        }

        public IFileInfoWrapper GetTransactionsLocation()
        {
            return new FileInfoWrapper(_transactionsLocation);
        }

        public IFileInfoWrapper GetWorkbookLocation()
        {
            return new FileInfoWrapper(_workbookLocation);
        }
    }
}
using BankReader.Data.Utilities;

namespace BankReader.Data.Providers
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly string _categoryRulesLocation;
        private readonly string _transactionsLocation;
        private readonly string _workbookLocation;

        public FileLocationProvider(string categoryRulesLocation, string transactionsLocation, string workbookLocation)
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
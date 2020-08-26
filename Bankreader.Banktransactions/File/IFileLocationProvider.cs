using BankReader.Data.Utilities;

namespace BankReader.Data.Providers
{
    public interface IFileLocationProvider
    {
        IFileInfoWrapper GetCategoryRulesLocation();

        IFileInfoWrapper GetTransactionsLocation();

        IFileInfoWrapper GetWorkbookLocation();
    }
}
namespace Bankreader.Application.Interfaces
{
    public interface IFileLocationProvider
    {
        IFileInfoWrapper GetCategoryRulesLocation();

        IFileInfoWrapper GetTransactionsLocation();

        IFileInfoWrapper GetWorkbookLocation();
    }
}
using BankReader.Shared.Models;

namespace BankReader.Implementation.Services.Interfaces
{
    public interface ITransactionCategorizer
    {
        Category DetermineCategory(string description);
    }
}
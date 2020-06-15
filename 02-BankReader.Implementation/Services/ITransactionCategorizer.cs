using BankReader.Data.Models;

namespace BankReader.Implementation.Services
{
    public interface ITransactionCategorizer
    {
        Category DetermineCategory(string description);
    }
}
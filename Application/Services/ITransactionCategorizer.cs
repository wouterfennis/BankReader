using Bankreader.Application.Models;

namespace Bankreader.Application.Services
{
    public interface ITransactionCategorizer
    {
        Category DetermineCategory(string description);
    }
}
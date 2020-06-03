using BankReader.Implementation.Models;
using System.Collections.Generic;

namespace BankReader.Implementation.Services
{
    public interface ICategoryService
    {
        IEnumerable<HouseholdPost> Categorise();
    }
}

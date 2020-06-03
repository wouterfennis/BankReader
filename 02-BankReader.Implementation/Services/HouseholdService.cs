using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using System.Collections.Generic;

namespace BankReader.Implementation.Services
{
    internal class HouseholdService
    {
        private readonly ICategoryService _categoryService;

        public HouseholdService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        void PlaceBanktransactionsUnderHousholdPosts(IEnumerable<CategoryRule> categoryRules, IEnumerable<Banktransaction> transactions)
        {
            //foreach (var transaction in transactions)
            //{
            //    var category = _categoryService.DetermineCategory(transaction);
            //}
            //_categoryService.DetermineCategory();
        }
    }
}

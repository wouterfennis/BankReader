using System.Collections.Generic;
using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Implementation.Models;

namespace BankReader.Implementation.Services
{
    public class CategoryService : ICategoryService
    {
        public IEnumerable<HouseholdPost> Categorise(IEnumerable<CategoryRule> categoryRules, IEnumerable<Transaction> transactions)
        {
            var householdPosts = new List<HouseholdPost>();

            foreach (var transaction in transactions)
            {
                foreach (var categoryRule in categoryRules)
                {
                    if (categoryRule.Validate(transaction))
                    {
                        var houseHoldPost = householdPosts.SingleOrDefault(x => x.Category == categoryRule.Category);

                        if (houseHoldPost == null)
                        {
                            houseHoldPost = new HouseholdPost(categoryRule.Category);
                            householdPosts.Add(houseHoldPost);
                        }
                        var householdTransaction = houseHoldPost.Transactions.SingleOrDefault(x => x.Date.Month == transaction.Date.Month);

                        if (householdTransaction == null)
                        {
                            householdTransaction = new HouseholdTransaction(transaction.Amount, transaction.Date, transaction.TransactionDirection);
                            houseHoldPost.Transactions.Add(householdTransaction);
                        }
                        else
                        {
                            householdTransaction.RaiseAmount(transaction.Amount);
                        }
                    }
                }
            }
            return householdPosts;
        }
    }
}

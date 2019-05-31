using System.Collections.Generic;
using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Implementation.Models;

namespace BankReader.Implementation.Services
{
    public class CategoryService : ICategoryService
    {
        public IEnumerable<HouseholdPost> Categorise(IList<CategoryRule> rules, IList<Transaction> transactions)
        {
            var householdPosts = new List<HouseholdPost>();

            foreach (var transaction in transactions)
            {
                foreach (var categoryRule in rules)
                {
                    if (categoryRule.Validate(transaction))
                    {
                        var houseHoldPost = householdPosts.SingleOrDefault(x => x.Category == categoryRule.Category);

                        if (houseHoldPost == null)
                        {
                            householdPosts.Add(new HouseholdPost
                            {
                                Category = categoryRule.Category,
                                Transactions = new List<HouseholdTransaction>
                                {
                                    new HouseholdTransaction
                                    {
                                        Amount = transaction.Amount,
                                        Date = transaction.Date,
                                        TransactionDirection = transaction.TransactionDirection
                                    }
                                }
                            });
                        }
                        else
                        {
                            var householdTransaction = houseHoldPost.Transactions.SingleOrDefault(x => x.Date.Month == transaction.Date.Month);
                            if (householdTransaction == null)
                            {
                                houseHoldPost.Transactions.Add(new HouseholdTransaction
                                {
                                    Amount = transaction.Amount,
                                    Date = transaction.Date,
                                    TransactionDirection = transaction.TransactionDirection
                                });
                            }
                            else
                            {
                                householdTransaction.Amount = householdTransaction.Amount + transaction.Amount;
                            }
                        }
                    }
                }
            }

            return householdPosts;
        }
    }
}

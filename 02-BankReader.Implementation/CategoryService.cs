using System.Collections.Generic;
using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using BankReader.Implementation.Models;

namespace BankReader.Implementation
{
    public class CategoryService : ICategoryService
    {
        private IEnumerable<CategoryRule> _categoryRules;

        public CategoryService(IEnumerable<CategoryRule> categoryRules)
        {
            _categoryRules = categoryRules;
        }

        public IEnumerable<HouseholdPost> Categorise(IEnumerable<Transaction> transactions)
        {
            var householdPosts = new List<HouseholdPost>();

            foreach (var transaction in transactions)
            {
                foreach (var categoryRule in _categoryRules)
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

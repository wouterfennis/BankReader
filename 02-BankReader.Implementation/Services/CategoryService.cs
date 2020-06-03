using System;
using System.Collections.Generic;
using System.Linq;
using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Data.Json;
using BankReader.Data.Models;
using BankReader.Implementation.Extensions;
using BankReader.Implementation.Models;

namespace BankReader.Implementation.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRuleProvider _categoryRuleProvider;
        private readonly ITransactionProvider _transactionProvider;

        public CategoryService(ICategoryRuleProvider categoryRuleProvider, ITransactionProvider transactionProvider)
        {
            _categoryRuleProvider = categoryRuleProvider;
            _transactionProvider = transactionProvider;
        }

        public IEnumerable<HouseholdPost> Categorise()
        {
            var transactions = _transactionProvider.ProvideTransactions();
            var categoryRules = _categoryRuleProvider.ProvideRules();

            var householdPosts = new List<HouseholdPost>();

            foreach (Banktransaction transaction in transactions.ToList())
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
                        transactions.Remove(transaction);
                    }
                }
            }

            var unknownTransactions = transactions.Select(x => new HouseholdTransaction(x.Amount, x.Date, x.TransactionDirection));
            var unknownHouseholdPost = new HouseholdPost(Category.Unknown);
            unknownHouseholdPost.Transactions.AddRange(unknownTransactions);
            householdPosts.Add(unknownHouseholdPost);
            return householdPosts;
        }
    }
}

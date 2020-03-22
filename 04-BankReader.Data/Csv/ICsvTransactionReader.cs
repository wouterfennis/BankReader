using System.Collections.Generic;
using BankReader.Data.Csv.Models;

namespace BankReader.Data.Csv
{
    /// <summary>
    /// A reader for Transactions
    /// </summary>
    public interface ICsvTransactionReader
    {
        /// <summary>
        /// Reads and parses a CSV to a Transaction
        /// </summary>
        /// <param name="filePath">The path of the CSV</param>
        IList<Banktransaction> ReadTransactions();
    }
}
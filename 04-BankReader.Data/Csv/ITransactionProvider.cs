using System.Collections.Generic;
using BankReader.Data.Csv.Models;

namespace BankReader.Data.Csv
{
    /// <summary>
    /// A provider for <see cref="Banktransaction"/>'s
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        /// Reads and parses data from the bank to <see cref="Banktransaction"/>'s
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        IList<Banktransaction> ProvideTransactions();
    }
}
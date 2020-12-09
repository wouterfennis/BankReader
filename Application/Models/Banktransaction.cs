using Bankreader.Domain.Models;
using System;

namespace Bankreader.Application.Models
{
    /// <summary>
    /// The definition of a banktransaction
    /// </summary>
    public class Banktransaction
    {
        /// <summary>
        /// The date the transaction took place.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The description provided by the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The accountnumber where the transaction is registed.
        /// </summary>
        public string Accountnumber { get; set; }

        /// <summary>
        /// The accountnumber of a third party that receives or deposits money.
        /// </summary>
        public string ContraAccountnumber { get; set; }

        /// <summary>
        /// The bank code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The direction the transaction is headed.
        /// </summary>
        public TransactionDirection TransactionDirection { get; set; }

        /// <summary>
        /// The amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The platform that is used to create the transaction.
        /// </summary>
        public string MutationType { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The balance after the mutation was completed.
        /// </summary>
        public decimal BalanceAfterMutation { get; set; }

        /// <summary>
        /// The custom tag associated with this transaction.
        /// </summary>
        public string Tag { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BankReader.Data.Models
{
    public sealed class YearMonth : ValueObject
    {
        public YearMonth(int year, int month)
        {
            Year = year;
            Month = month;
        }

        /// <summary>
        /// Gives year part.
        /// </summary>
        public Year Year { get; }

        /// <summary>
        /// Gives month part.
        /// </summary>
        public Month Month { get; }

        /// <summary>
        /// Creates a YearMonth from a DateTime
        /// </summary>
        public static YearMonth FromDateTime(DateTime date) => new YearMonth(date.Year, date.Month + 1);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Year, Month };
    }
}

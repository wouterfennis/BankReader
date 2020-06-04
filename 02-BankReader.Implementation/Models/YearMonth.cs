using System;
using System.Collections.Generic;
using System.Text;

namespace BankReader.Implementation.Models
{
    internal sealed class YearMonth : ValueObject
    {
        private const int MinimalMonth = 1;
        private const int MaximalMonth = 12;

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
        /// Creates a YearMonth from integers
        /// </summary>
        public static YearMonth FromInt32(int year, int month) => new YearMonth(year, month);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Year, Month };
    }
}

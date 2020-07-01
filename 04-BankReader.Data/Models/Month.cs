using System;
using System.Collections.Generic;

namespace BankReader.Data.Models
{
    /// <summary>
    /// Value object of a Month
    /// </summary>
    public sealed class Month : ValueObject
    {
        private const int MinimalMonth = 1;
        private const int MaximalMonth = 12;

        private Month(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < MinimalMonth || value > MaximalMonth)
            {
                throw new ArgumentException("The value was not a valid month", nameof(value));
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a Month from an int
        /// </summary>
        public static implicit operator Month(int value) => FromInt32(value);

        /// <summary>
        /// Creates a Month from an int
        /// </summary>
        public static Month FromInt32(int value) => new Month(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}

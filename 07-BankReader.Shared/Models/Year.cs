using System;
using System.Collections.Generic;

namespace BankReader.Shared.Models
{
    /// <summary>
    /// Value Object of a Year
    /// </summary>
    public sealed class Year : ValueObject
    {
        private const int MinimalYear = 2000;
        private const int MaximalYear = 2100;

        private Year(int value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(int value)
        {
            if (value < MinimalYear || value > MaximalYear)
            {
                throw new ArgumentException("The value was not a valid year", nameof(value));
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Creates a Year from an int
        /// </summary>
        public static implicit operator Year(int value) => FromInt32(value);

        /// <summary>
        /// Creates a Year from an int
        /// </summary>
        public static Year FromInt32(int value) => new Year(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}

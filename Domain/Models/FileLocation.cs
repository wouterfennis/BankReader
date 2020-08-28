using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bankreader.Domain.Models
{
    public class FilePath : ValueObject
    {
        private FilePath(string value)       {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The value was not a valid file path", nameof(value));
            }
        }

        /// <summary>
        /// Gives raw value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Creates a FilePath from an int
        /// </summary>
        public static implicit operator FilePath(string value) => FromString(value);

        /// <summary>
        /// Creates a FilePath from an int
        /// </summary>
        public static FilePath FromString(string value) => new FilePath(value);

        protected override IEnumerable<object> GetEqualityComponents() => new object[] { Value };
    }
}

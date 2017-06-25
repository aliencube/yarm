using System;

namespace Yarm.Extensions
{
    /// <summary>
    /// This represents the extension class for <see cref="int"/>.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Checks whether the given value is less than or equal to the comparer value or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        /// <returns>Returns <c>True</c>, if the value is less than or equal to the comparer; otherwise returns <c>False</c>.</returns>
        public static bool IsLessThanOrEqualTo(this int value, int comparer)
        {
            return value <= comparer;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the given value is less than or equal to the given comparer.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="comparer">Value to compare.</param>
        /// <returns>Returns the original value, if the value is greater than the comparer; otherwise throws an <see cref="ArgumentOutOfRangeException"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than or equal to <paramref name="comparer"/>.</exception>
        public static int ThrowIfLessThanOrEqualTo(this int value, int comparer)
        {
            if (value.IsLessThanOrEqualTo(comparer))
            {
                throw new ArgumentOutOfRangeException();
            }

            return value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Yarm.Extensions
{
    /// <summary>
    /// This represents the extension class for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether the string value is either <c>null</c> or white space.
        /// </summary>
        /// <param name="value"><see cref="string"/> value to check.</param>
        /// <returns>Returns <c>True</c>, if the string value is either <c>null</c> or white space; otherwise returns <c>False</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given value is <c>null</c> or white space.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Returns the original value, if the value is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static string ThrowIfNullOrWhiteSpace(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }

        /// <summary>
        /// Checks whether the string value is equal to the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value is equal to the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool IsEquivalentTo(this string value, string comparer)
        {
            return value.Equals(comparer, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Converts the string value to <see cref="int"/> value.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Returns the <see cref="int"/> value converted.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static int ToInt32(this string value)
        {
            value.ThrowIfNullOrWhiteSpace();

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts the string value to <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Returns the <see cref="bool"/> value converted.</returns>
        public static bool ToBoolean(this string value)
        {
            return !value.IsNullOrWhiteSpace() && Convert.ToBoolean(value);
        }

        /// <summary>
        /// Encodes the string value.
        /// </summary>
        /// <param name="value">String value to encode.</param>
        /// <returns>Returns URL encoded value.</returns>
        public static string UrlEncode(this string value)
        {
            return value.IsNullOrWhiteSpace()
                ? value
                : WebUtility.UrlEncode(value).Replace("+", "%20");
        }

        /// <summary>
        /// Decodes the string value.
        /// </summary>
        /// <param name="value">String value to decode.</param>
        /// <returns>Returns URL decoded value.</returns>
        public static string UrlDecode(this string value)
        {
            return value.IsNullOrWhiteSpace()
                ? value
                : WebUtility.UrlDecode(value.Replace("%20", "+"));
        }

        /// <summary>
        /// Checks whether the given list of items contains the item or not, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value contains the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool ContainsEquivalent(this string value, string comparer)
        {
            value.ThrowIfNullOrWhiteSpace();

            return !comparer.IsNullOrWhiteSpace() && value.ToLowerInvariant().Contains(comparer.ToLowerInvariant());
        }

        /// <summary>
        /// Checks whether the given list of items contains the item or not, regardless of casing.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <param name="item">Item to check.</param>
        /// <returns>Returns <c>True</c>, if the list of items contains the item; otherwise returns <c>False</c>.</returns>
        public static bool ContainsEquivalent(this IEnumerable<string> items, string item)
        {
            items.ThrowIfNullOrDefault();

            return items.Any(p => p.IsEquivalentTo(item));
        }

        /// <summary>
        /// Checks whether the string value starts with the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value starts with the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool StartsWithEquivalent(this string value, string comparer)
        {
            value.ThrowIfNullOrWhiteSpace();

            return !comparer.IsNullOrWhiteSpace() && value.StartsWith(comparer, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Checks whether the string value ends with the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value ends with the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool EndsWithEquivalent(this string value, string comparer)
        {
            value.ThrowIfNullOrWhiteSpace();

            return !comparer.IsNullOrWhiteSpace() && value.EndsWith(comparer, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

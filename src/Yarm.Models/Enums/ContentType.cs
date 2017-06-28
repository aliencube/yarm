using System.Collections.Generic;
using System.Linq;

using Yarm.Extensions;

namespace Yarm.Models.Enums
{
    /// <summary>
    /// This specifies the content type of the GitHub content.
    /// </summary>
    public class ContentType : TypeSafeEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentType"/> class.
        /// </summary>
        /// <param name="name">Enum name.</param>
        /// <param name="value">Enum value.</param>
        protected ContentType(string name, int value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Identifies the content type as <c>file</c>.
        /// </summary>
        public static ContentType File => new ContentType("file", 1);

        /// <summary>
        /// Identifies the content type as <c>dir</c>.
        /// </summary>
        public static ContentType Directory => new ContentType("dir", 2);

        /// <summary>
        /// Gets the list of all enums.
        /// </summary>
        /// <returns>Returns the list of all enums.</returns>
        public static IEnumerable<ContentType> GetAll()
        {
            return new[] { File, Directory };
        }

        /// <summary>
        /// Parses the string into the <see cref="ContentType"/> object.
        /// </summary>
        /// <param name="name">Name to parse.</param>
        /// <returns>Returns the <see cref="ContentType"/> object parsed.</returns>
        public static ContentType Parse(string name)
        {
            return name.IsNullOrWhiteSpace()
                       ? null
                       : ContentType.GetAll().SingleOrDefault(p => p.Name.IsEquivalentTo(name));
        }

        /// <summary>
        /// Parses the value into the <see cref="ContentType"/> object.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <returns>Returns the <see cref="ContentType"/> object parsed.</returns>
        public static ContentType Parse(int value)
        {
            return ContentType.GetAll().SingleOrDefault(p => p.Value == value);
        }

        /// <summary>
        /// Converts the instance to string value implicitly.
        /// </summary>
        /// <param name="name">Enum name.</param>
        /// <returns>Returns the string value implicitly converted.</returns>
        public static implicit operator ContentType(string name)
        {
            return ContentType.GetAll().SingleOrDefault(p => StringExtensions.IsEquivalentTo(p.Name, name));
        }

        /// <summary>
        /// Converts the instance to string value implicitly.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns>Returns the string value implicitly converted.</returns>
        public static implicit operator ContentType(int value)
        {
            return ContentType.GetAll().SingleOrDefault(p => p.Value == value);
        }
    }
}
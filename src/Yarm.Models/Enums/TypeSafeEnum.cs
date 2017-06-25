using Yarm.Extensions;

namespace Yarm.Models.Enums
{
    /// <summary>
    /// This represents the type-safe enum entity.
    /// </summary>
    public abstract class TypeSafeEnum
    {
        private readonly string _name;
        private readonly int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSafeEnum"/> class.
        /// </summary>
        /// <param name="name">Enum name.</param>
        /// <param name="value">Enum value.</param>
        protected TypeSafeEnum(string name, int value)
        {
            this._name = name.ThrowIfNullOrWhiteSpace();
            this._value = value.ThrowIfLessThanOrEqualTo(-1);
        }

        /// <summary>
        /// Gets the enum name.
        /// </summary>
        public string Name => this._name;

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        public int Value => this._value;

        /// <summary>
        /// Converts the instance to string value implicitly.
        /// </summary>
        /// <param name="instance"><see cref="TypeSafeEnum"/> instance.</param>
        /// <returns>Returns the string value implicitly converted.</returns>
        public static implicit operator string(TypeSafeEnum instance)
        {
            return instance.ToString();
        }

        /// <summary>
        /// Converts the instance to integer value implicitly.
        /// </summary>
        /// <param name="instance"><see cref="TypeSafeEnum"/> instance.</param>
        /// <returns>Returns the integer value implicitly converted.</returns>
        public static implicit operator int(TypeSafeEnum instance)
        {
            return instance._value;
        }

        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>A string that represents the current instance.</returns>
        public override string ToString()
        {
            return this._name;
        }
    }
}
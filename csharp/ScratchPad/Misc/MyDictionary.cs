using System.Collections.Generic;

namespace ScratchPad.Misc
{
    /// <inheritdoc />
    /// <summary>
    ///     Extends the dictionary class to allow duplicate values to be added to a dictionary for a single key.
    /// </summary>
    /// <remarks>
    ///     Note: This has not be tested with all of the dictionary methods.
    /// </remarks>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal class MyDictionary<TKey, TValue> : Dictionary<TKey, IList<TValue>>
    {
        /// <summary>
        ///     Gets or sets a value for an existing key and value index.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="index">The index.</param>
        internal TValue this[TKey key, int index = 0]
        {
            get => base[key][index];
            set => base[key][index] = value;
        }

        /// <summary>
        ///     Allows duplicate values to be added to a dictionary for a single key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value as a list of type TValue.</param>
        internal void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                base[key].Add(value);
            }
            else
            {
                var list = new List<TValue>();
                list.Add(value);
                base.Add(key, list);
            }
        }

        /// <summary>
        ///     Gets a value for a key and given index.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal TValue Get(TKey key, int index = 0)
        {
            return base[key][index];
        }
    }
}

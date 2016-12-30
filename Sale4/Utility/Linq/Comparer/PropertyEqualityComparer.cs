using System;
using System.Collections.Generic;

namespace Utility.Linq.Comparer
{
    /// <summary>
    /// 属性比较器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class PropertyEqualityComparer<T, TKey> : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> keySelector;
        private readonly IEqualityComparer<TKey> comparer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        public PropertyEqualityComparer(Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        public PropertyEqualityComparer(Func<T, TKey> keySelector)
            : this(keySelector, EqualityComparer<TKey>.Default)
        { }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
}

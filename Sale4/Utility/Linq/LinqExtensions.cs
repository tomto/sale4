using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Linq.Comparer;

namespace Utility.Linq
{
    /// <summary>
    /// linq扩展方法
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 根据指定的字段去重
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.Distinct(new PropertyEqualityComparer<TSource, TKey>(keySelector));
        }



        /// <summary>
        /// 根据指定的字段去重
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.Distinct(new PropertyEqualityComparer<TSource, TKey>(keySelector, comparer));
        }

        /// <summary>
        /// 通过使用默认的相等比较器对值进行比较生成两个序列的差集
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> Except<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            return first.Except(second, new PropertyEqualityComparer<TSource, TKey>(keySelector));
        }

        /// <summary>
        /// 通过使用默认的相等比较器对值进行比较生成两个序列的交集
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> Intersect<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            return first.Intersect(second, new PropertyEqualityComparer<TSource, TKey>(keySelector));
        }
    }
}

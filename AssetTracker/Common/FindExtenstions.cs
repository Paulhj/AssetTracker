using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common
{
    public static class FindExtenstions
    {
        public static IQueryable<TSource> FindIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<TSource> FindIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}

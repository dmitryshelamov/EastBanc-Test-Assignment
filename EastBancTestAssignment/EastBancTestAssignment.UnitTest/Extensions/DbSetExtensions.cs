using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;

namespace EastBancTestAssignment.UnitTest.Extensions
{
    public static class DbSetExtensions
    {
        public static void SetSource<T>(this IDbSet<T> dbSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());
        }
    }
}

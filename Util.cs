using System.Linq.Expressions;
using System.Reflection;

namespace App
{
    public static class Util
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, string column, string direction)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type);
            var property = type.GetProperty(column, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            var member = Expression.MakeMemberAccess(parameter, property);
            var lamda = Expression.Lambda(member, parameter);
            var method = direction == "desc" ? "OrderByDescending" : "OrderBy";
            var expression = Expression.Call(typeof(Queryable), method, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(lamda));
            return query.Provider.CreateQuery<TEntity>(expression);
        }
    }
}
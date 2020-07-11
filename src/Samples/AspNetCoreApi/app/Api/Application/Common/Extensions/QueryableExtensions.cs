using System;
using System.Linq;
using System.Linq.Expressions;
using ApiTemplate.Api.Application.Common.Paging;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Expressions.Expression;

namespace ApiTemplate.Api.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> AddQuery<TEntity>(this IQueryable<TEntity> queryable, PagedQuery query)
        {
            if (query.HasSearch)
            {
                queryable = queryable.Search(query.FilterField, query.FilterText);
            }

            if (query.OrderBy != null)
            {
                queryable = queryable.OrderBy(query.OrderBy, query.OrderByDesc);
            }

            return queryable;
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> source, string propertyName, string searchTerm)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(searchTerm))
            {
                return source;
            }

            var property = typeof(T).GetProperty(propertyName);

            if (property is null)
            {
                return source;
            }

            searchTerm = "%" + searchTerm + "%";
            var itemParameter = Parameter(typeof(T), "item");

            var functions = Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
            var like = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });

            Expression expressionProperty = Property(itemParameter, property.Name);

            if (property.PropertyType != typeof(string))
            {
                expressionProperty = Call(expressionProperty, typeof(object).GetMethod(nameof(object.ToString), new Type[0]));
            }

            var selector = Call(
                null,
                like,
                functions,
                expressionProperty,
                Constant(searchTerm));

            return source.Where(Lambda<Func<T, bool>>(selector, itemParameter));
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            if (string.IsNullOrEmpty(propertyName))
                return source;

            var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (property is null)
                return source;

            if (descending)
                return source.OrderByDescending(ToLambda<T>(propertyName));
            else
                return source.OrderBy(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}

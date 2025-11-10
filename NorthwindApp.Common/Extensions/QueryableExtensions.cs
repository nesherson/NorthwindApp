using System.Linq.Expressions;

namespace NorthwindApp.Common;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByDynamic<T>(
        this IQueryable<T> source, 
        string propertyName, 
        string sortOrder)
    {
        var command = sortOrder == "desc" ? nameof(Queryable.OrderByDescending) 
            : nameof(Queryable.OrderBy);
        
        if (source.Expression.Type == typeof(IOrderedQueryable<T>))
        {
            command = sortOrder == "desc" ? nameof(Queryable.ThenByDescending) 
                : nameof(Queryable.ThenBy) ;
        }
        
        var parameter = Expression.Parameter(typeof(T), "x");
        Expression propertyAccess = parameter;
        
        foreach (var property in propertyName.Split('.'))
        {
            propertyAccess = Expression.PropertyOrField(propertyAccess, property);
        }
        
        var lambda = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(
            typeof(Queryable),  
            command,           
            [typeof(T), propertyAccess.Type], 
            source.Expression,
            Expression.Quote(lambda)
        );
        
        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(resultExpression);
    }
}
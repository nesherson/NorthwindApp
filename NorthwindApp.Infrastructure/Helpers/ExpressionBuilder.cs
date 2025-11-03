namespace NorthwindApp.Infrastructure;

public class ExpressionBuilder
{
    // public static IOrderedQueryable<T> ApplyOrderBy<T>(IQueryable<T> source,
    //     string property,
    //     SortOrder sortOrder)
    //     {
    //         var methodName = sortOrder == SortOrder.Descending ?
    //            nameof(Queryable.OrderByDescending) :
    //            nameof(Queryable.OrderBy);
    //         Type type = typeof(T);
    //         ParameterExpression arg = Expression.Parameter(type, "x");
    //         Expression expr = arg;
    //         PropertyInfo pi = type.GetProperty(string.Concat(property[0].ToString().ToUpper(), property.AsSpan(1)));
    //
    //         if (pi == null)
    //             return (IOrderedQueryable<T>)source;
    //
    //         expr = Expression.Property(expr, pi);
    //         type = pi.PropertyType;
    //         Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
    //         LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
    //
    //         object result = typeof(Queryable).GetMethods().Single(
    //                 method => method.Name == methodName
    //                         && method.IsGenericMethodDefinition
    //                         && method.GetGenericArguments().Length == 2
    //                         && method.GetParameters().Length == 2)
    //                 .MakeGenericMethod(typeof(T), type)
    //                 .Invoke(null, new object[] { source, lambda });
    //         return (IOrderedQueryable<T>)result;
    //     }
}

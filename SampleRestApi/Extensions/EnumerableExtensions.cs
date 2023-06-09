using SampleRestApi.Enums;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SampleRestApi.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");
}

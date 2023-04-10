namespace EFCore.Shared.Utility;
public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime me, DateTime startDate, DateTime endDate) => startDate < me && me < endDate;
    public static bool IsBetweenIncluded(this DateTime me, DateTime startDate, DateTime endDate) => startDate <= me && me <= endDate;
}

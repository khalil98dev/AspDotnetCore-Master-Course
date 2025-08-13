
using System.Diagnostics.CodeAnalysis;

public class DateRange : IParsable<DateRange>
{
    public DateOnly From { get; init; }
    public DateOnly To { get; init; }
    public static DateRange Parse(string value, IFormatProvider? provider)
    {
        if (!TryParse(value, provider, out var resutl))
        {
            throw new ArgumentException("We cant parse the data To DateRange Type");
        }

        return resutl;
    }

    public static bool TryParse([NotNullWhen(true)] string? Query, IFormatProvider? provider,
    [MaybeNullWhen(false)] out DateRange result)
    {
        var Obj = Query?.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (Obj?.Length == 2
        && DateOnly.TryParse(Obj[0].ToString(), provider, out var from)
        && DateOnly.TryParse(Obj[1].ToString(), provider, out var to)
        )
        {
            result = new DateRange
            {
                From = from,
                To = to
            };

            return true;
        }

        result = new DateRange
        {
            From = default,
            To = default
        };
        return false;
    }
}
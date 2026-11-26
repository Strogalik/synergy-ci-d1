namespace Tariffs;

public sealed class RateSource : IRateSource
{
    private readonly Dictionary<string, decimal> rates = new(StringComparer.Ordinal);
    public RateSource(IEnumerable<Rate> source)
    {
        foreach (var rate in source)
            rates.TryAdd(rate.Code, rate.Price);
    }

    public decimal Find(string code)
    {
        if (rates.TryGetValue(code, out var price)) return price;
        throw new KeyNotFoundException(code);
    }
}

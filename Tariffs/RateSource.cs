namespace Tariffs;

public sealed class RateSource : IRateSource
{
    private readonly Rate[] rates;
    public RateSource(IEnumerable<Rate> rates) => this.rates = rates.ToArray();

    public decimal Find(string code)
    {
        foreach (var rate in rates)
            if (rate.Code == code) return rate.Price;
        throw new KeyNotFoundException(code);
    }
}

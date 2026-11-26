namespace Tariffs;

public sealed record Rate(string Code, decimal Price);
public sealed record Quote(string Code, int Units, decimal Total);
public interface IRateSource
{
    decimal Find(string code);
}
public sealed class Calculator
{
    private readonly IRateSource rates;
    public Calculator(IRateSource rates) => this.rates = rates;

    public Quote Calculate(string code, int units)
    {
        if (units <= 0) throw new ArgumentOutOfRangeException(nameof(units));
        return new Quote(code, units, rates.Find(code) * units);
    }
}

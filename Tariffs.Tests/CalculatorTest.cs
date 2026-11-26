using Tariffs;
using Xunit;

public class CalculatorTest
{
    [Fact]
    public void CalculatesKnownCode()
    {
        var calculator = new Calculator(new RateSource(new[] { new Rate("A", 2m) }));
        Assert.Equal(new Quote("A", 3, 6m), calculator.Calculate("A", 3));
    }
}

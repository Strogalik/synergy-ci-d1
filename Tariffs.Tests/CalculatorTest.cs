using Moq;
using Tariffs;
using Xunit;

public class CalculatorTest
{
    [Fact]
    public void CalculatesKnownCodeUsingInjectedRate()
    {
        var mock = new Mock<IRateSource>(MockBehavior.Strict);
        mock.Setup(rates => rates.Find("A")).Returns(2m);
        var calculator = new Calculator(mock.Object);

        Assert.Equal(new Quote("A", 3, 6m), calculator.Calculate("A", 3));
        mock.Verify(rates => rates.Find("A"), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RejectsNonPositiveUnitsBeforeLookup(int units)
    {
        var source = new Mock<IRateSource>(MockBehavior.Strict);
        var calculator = new Calculator(source.Object);
        Assert.Equal("units", Assert.Throws<ArgumentOutOfRangeException>(
            () => calculator.Calculate("A", units)).ParamName);
        source.VerifyNoOtherCalls();
    }

    [Fact]
    public void ThrowsForUnknownCode()
    {
        var source = new RateSource(Array.Empty<Rate>());
        Assert.Throws<KeyNotFoundException>(() => new Calculator(source).Calculate("A", 1));
    }

    [Fact]
    public void UsesFirstRateAndOrdinalComparison()
    {
        var source = new RateSource(new[] { new Rate("A", 2m), new Rate("A", 10m) });
        Assert.Equal(2m, source.Find("A"));
        Assert.Throws<KeyNotFoundException>(() => source.Find("a"));
    }
}

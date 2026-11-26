using System.Diagnostics;
using Tariffs;
var rates = Enumerable.Range(0, 1024).Select(i => new Rate($"T{i}", i + 1m)).ToArray();
var calculator = new Calculator(new RateSource(rates));
string[] queries = Enumerable.Range(0, 1024).Select(i => $"T{i}").ToArray();
decimal Run(int count)
{
    decimal sum = 0;
    for (int i = 0; i < count; i++) sum += calculator.Calculate(queries[i % queries.Length], 2).Total;
    return sum;
}
Run(20000);
Console.WriteLine("repeat,elapsed_ms,checksum");
for (int i = 1; i <= 7; i++)
{
    var clock = Stopwatch.StartNew();
    var checksum = Run(150000);
    clock.Stop();
    Console.WriteLine(FormattableString.Invariant($"{i},{clock.Elapsed.TotalMilliseconds:F4},{checksum}"));
}

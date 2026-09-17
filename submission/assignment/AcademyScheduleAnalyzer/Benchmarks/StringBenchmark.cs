using System.Text;
using BenchmarkDotNet.Attributes;

// ======================================================
// Part 21 — BenchmarkDotNet
// Part 22 — Different Loop Sizes
// Part 23 — Memory Usage
// Part 24 — Benchmark Rules
// ======================================================

[MemoryDiagnoser]
public class StringBenchmark
{
    [Params(100, 1000, 10000, 100000)]
    public int Iterations;

    [Benchmark]
    public string StringConcatenation()
    {
        string result = "";

        for (int i = 0; i < Iterations; i++)
        {
            result += "Academy Session";
        }

        return result;
    }

    [Benchmark]
    public string StringBuilderConcatenation()
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < Iterations; i++)
        {
            result.Append("Academy Session");
        }

        return result.ToString();
    }
}
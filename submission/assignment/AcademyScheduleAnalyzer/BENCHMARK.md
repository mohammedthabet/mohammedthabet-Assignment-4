# Benchmark Results

This benchmark compares repeated string concatenation using `string` with repeated appending using `StringBuilder`.

The benchmark was executed using BenchmarkDotNet with four different iteration counts:

- 100
- 1,000
- 10,000
- 100,000

## Performance Results

| Iterations | String Concatenation | StringBuilder |
|-----------:|---------------------:|--------------:|
| 100 | 8.656 µs | 0.746 µs |
| 1,000 | 858.846 µs | 5.135 µs |
| 10,000 | 130.728 ms | 190.647 µs |
| 100,000 | 25.545 s | 1.796 ms |

## Memory Allocation Results

| Iterations | String Concatenation | StringBuilder |
|-----------:|---------------------:|--------------:|
| 100 | 150.34 KB | 7.52 KB |
| 1,000 | 14,687.45 KB | 61.72 KB |
| 10,000 | 1,465,373.69 KB | 592.24 KB |
| 100,000 | 146,495,504.23 KB | 5,882.92 KB |

## Benchmark Analysis

### Performance

**1. Which approach was faster with 100 iterations?**

`StringBuilder` was faster.

- String concatenation: 8.656 µs
- StringBuilder: 0.746 µs

For this benchmark, StringBuilder completed the same repeated-appending workload in less time.

**2. Which approach was faster with 100,000 iterations?**

`StringBuilder` was significantly faster.

- String concatenation: 25.545 s
- StringBuilder: 1.796 ms

The performance difference became much larger as the number of repeated concatenations increased.

**3. What happened to string concatenation performance as the loop size increased?**

The execution time of repeated string concatenation increased dramatically as the number of iterations increased:

- 100 → 8.656 µs
- 1,000 → 858.846 µs
- 10,000 → 130.728 ms
- 100,000 → 25.545 s

This shows that repeated string concatenation scaled poorly in this benchmark as the workload became larger.
### Memory Usage

**4. Which approach allocated more memory?**

Repeated string concatenation allocated much more memory than `StringBuilder` in this benchmark.

For example, at 100,000 iterations:

- String concatenation: 146,495,504.23 KB
- StringBuilder: 5,882.92 KB

The difference also increased as the workload became larger.

**5. Why does repeated string concatenation create additional allocations?**

Strings in C# are immutable. Once a string object is created, its contents cannot be changed.

When an operation such as:

`result += "Academy Session";`

is repeated, the existing string cannot simply be modified in place. A new string must be produced containing the previous text plus the newly appended text.

As the string becomes larger, repeated concatenation can therefore create many additional allocations and copy increasingly large amounts of text.

**6. Why does StringBuilder usually perform better when text is repeatedly appended?**

`StringBuilder` is designed for building and modifying text incrementally.

Instead of creating a new string for every append operation, it maintains an internal character buffer that can be reused and expanded when necessary.

For example:

`result.Append("Academy Session");`

can append text to the builder without creating a completely new string for every operation.

The final string is created when:

`result.ToString();`

is called.

This reduces the amount of repeated allocation and copying, which is why StringBuilder performed much better for the large repeated-appending workloads in this benchmark.
### When Should StringBuilder Be Used?

**7. Is StringBuilder always better than normal string operations? Explain.**

No. `StringBuilder` is not always the better choice.

Normal string operations are appropriate when working with a small number of strings or when only a few concatenations are required. In these cases, using normal string operations is simple, readable, and often sufficient.

`StringBuilder` becomes especially useful when text is built incrementally through many repeated append operations, particularly inside loops or when constructing large amounts of text.

The benchmark in this assignment demonstrates this difference for repeated concatenation. As the number of iterations increased, `StringBuilder` performed much better and allocated much less memory than repeated string concatenation.

Therefore, the choice should depend on the workload rather than assuming that `StringBuilder` is always faster.
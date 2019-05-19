# Uma.Uuid

[![pipeline status](https://gitlab.com/1ma/uma.uuid/badges/master/pipeline.svg)](https://gitlab.com/1ma/uma.uuid/pipelines)
[![coverage report](https://gitlab.com/1ma/uma.uuid/badges/master/coverage.svg)](https://gitlab.com/1ma/uma.uuid/commits/master)

A port of the [uma/uuid] library to C#, an [RFC 4122] compliant implementation of Universally Unique Identifiers.

This software is in alpha stage.

### Installation

```
dotnet add package Uma.Uuid --version 0.1.0 
```

### How does `Uma.Uuid` compare to the standard `Guid` class?

There are three key benefits:

#### Generation decoupled from the value object

Withs Guids you generate new value objects calling the static `Guid.NewGuid()` method. With `Uma.Uuid` generation is
segregated into a `IUuidGenerator` interface, meaning you can easily swap implementations and generation strategies
in your services.

#### Multiple generation strategies

`Guid.NewGuid()` always generates [version 4] Guids, which are not ideal especially if you use them as table
[primary keys] in a relational database.

`Uma.Uuid` provides several [RFC 4122] compliant generators (including version 4) plus Jimmy Nilsson's [COMB Guid generator]
and a SequentialGenerator for testing scenarios where determinism might be preferred.

#### Consistent Big-Endian serialization

When converting a `Guid` to a 16-byte array (for instance, to store it in a table in compressed form) with
`Guid.ToByteArray()` it does something unexpected: it serializes the higher 8 bytes, belonging to the first 3 groups in
little-endian form, while the lower 8 are serialized in big endian. Moreover it does not exhibit this behaviour when the
same object is serialized with `Guid.ToString()`.

In contrast `Uuid.ToByteArray()` and `Uuid.ToString()` always serialize the underlying bytes in Big-Endian, which is
specially important for COMB Uuids, otherwise their "monotonically increasing" property would be lost.

This can be illustrated with the following xUnit test (that is taken from the actual test suite):

```csharp
[Fact]
public void TestUuidGuidDivergence()
{
    var bytes = new byte[]
    {
        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10
    };

    var uuid = new Uuid(bytes);
    var guid = new Guid(bytes);

    Assert.Equal("01234567-89ab-cdef-fedc-ba9876543210", uuid.ToString());
    Assert.Equal("67452301-ab89-efcd-fedc-ba9876543210", guid.ToString());
}
```

### FAQ

### How do you convert an `Uuid` to a plain old `Guid`?

The value object has a helper method for that, `Uuid.ToGuid()`.

#### Which `IUuidGenerator` is the "best"?

Hands down the `CombGenerator`, for the reasons exposed by Jimmy Nilsson in his classical article [The Cost of GUIDs as Primary Keys].

### Can I write my own generators?

Yes, just write an implementation of the `IUuidGenerator`. Here's sample implementation that could be useful in a testing environment:

```csharp
using Uma.Uuid;

namespace Uuid.Sample
{
    public class DeterministicGenerator : IUuidGenerator
    {
        private readonly Uuid _uuid;

        public DeterministicGenerator(Uma.Uuid.Uuid uuid)
        {
            _uuid = uuid;
        }

        public Uuid NewUuid(string name = null)
        {
            return _uuid;
        }
    }
}
```

### Benchmarking

There is a [BenchmarkDotNet] suite under `Uma.Uuid.Benchmarks`. Currently `Guid` is much faster than `Uuid` when
constructing from a string, but about on par when constructing from a 16-byte array.

On the other hand all generators run generally faster than `Guid.NewGuid`. Here's a sample run on my laptop:

```
// * Summary *

BenchmarkDotNet=v0.11.5, OS=ubuntu 18.04
Intel Pentium CPU N4200 1.10GHz, 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=2.2.204
  [Host]     : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

|               Method |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
| CreateGuidFromString |   718.1 ns | 14.63 ns | 32.73 ns |   706.1 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| CreateUuidFromString | 3,555.1 ns | 68.76 ns | 64.32 ns | 3,512.0 ns |  4.85 |    0.23 | 0.3014 |     - |     - |     160 B |

|                  Method |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |---------:|----------:|----------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| CreateGuidFromByteArray | 22.55 ns | 0.0223 ns | 0.0174 ns | 22.55 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| CreateUuidFromByteArray | 31.45 ns | 1.9380 ns | 5.5605 ns | 29.27 ns |  1.25 |    0.13 | 0.0457 |     - |     - |      24 B |

|             Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|       GenerateGuid | 3,023.4 ns | 21.271 ns | 18.856 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|       GenerateComb | 2,181.5 ns |  4.718 ns |  3.940 ns |  0.72 |    0.00 | 0.3052 |     - |     - |     160 B |
|   GenerateVersion4 |   311.9 ns |  5.909 ns |  6.323 ns |  0.10 |    0.00 | 0.1216 |     - |     - |      64 B |
|   GenerateVersion5 | 1,885.8 ns | 57.314 ns | 56.290 ns |  0.63 |    0.02 | 0.4559 |     - |     - |     240 B |
| GenerateSequential |   113.1 ns |  2.242 ns |  5.707 ns |  0.04 |    0.00 | 0.1067 |     - |     - |      56 B |
```

### Contributing

This project is my first foray into C# and the .NET ecosystem. Suggestions, issues and PRs are always welcome.


[uma/uuid]: https://github.com/1ma/uuid
[RFC 4122]: https://tools.ietf.org/html/rfc4122
[version 4]: https://tools.ietf.org/html/rfc4122#section-4.4
[primary keys]: http://www.informit.com/articles/article.aspx?p=25862
[COMB Guid generator]: http://www.informit.com/articles/article.aspx?p=25862
[The Cost of GUIDs as Primary Keys]: http://www.informit.com/articles/article.aspx?p=25862
[BenchmarkDotNet]: https://benchmarkdotnet.org/

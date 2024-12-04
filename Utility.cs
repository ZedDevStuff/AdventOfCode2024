namespace AdventOfCode2024.Utility;

public static class Maths
{
    public static bool WithinInclusive(int value, int min, int max)
    {
        return value >= min && value <= max;
    }
    public static bool WithinExclusive(int value, int min, int max)
    {
        return value > min && value < max;
    }
}
public static class Fs
{
    public static string GetInput(int day)
    {
        try
        {
            return File.ReadAllText($"./Data/Day{day}/input.txt");
        }
        catch
        {
            return "";
        }
    }
    public static string GetTest(int day)
    {
        try
        {
            return File.ReadAllText($"./Data/Day{day}/test.txt");
        }
        catch
        {
            return "";
        }
    }

    public static string GetEdgeCases(int day)
    {
        try
        {
            return File.ReadAllText($"./Data/Day{day}/edge.txt");
        }
        catch
        {
            return "";
        }
    }
}
public static class Quick
{
    public static IntRange Range(int start, int end)
    {
        return new IntRange(start, end);
    }
    public static UIntRange Range(uint start, uint end)
    {
        return new UIntRange(start, end);
    }
    public static LongRange Range(long start, long end)
    {
        return new LongRange(start, end);
    }
}
public static class ConsoleEx
{
    public static void WriteColor(object message ,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void WriteColor(params (object message ,ConsoleColor color)[] values)
    {
        foreach (var (message, color) in values)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void WriteLineColor(object message ,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void WriteLineColor(params (object message ,ConsoleColor color)[] values)
    {
        foreach (var (message, color) in values)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
        }
        Console.Write("\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
public class LongRange : IEnumerable<long>
{
    private readonly long _start;
    private readonly long _end;
    public long Start => _start;
    public long End => _end;
    public long Length => (_end - _start - 1) < 0 ? (_end - _start - 1) * -1 : (_end - _start - 1);

    public LongRange(long start, long end)
    {
        _start = start;
        _end = end;
        for (long i = start; i <= end; i++)
        {
            this.Append(i);
        }
    }
    public bool Overlaps(LongRange range)
    {
        return IsWithin(range.Start) || IsWithin(range.End);
    }
    public bool IsWithin(long value)
    {
        return value >= _start && value <= _end;
    }
    public IEnumerator<long> GetEnumerator()
    {
        for (long i = _start; i <= _end; i++)
        {
            yield return i;
        }
    }
        
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class IntRange : IEnumerable<int>
{
    private readonly int _start;
    private readonly int _end;
    public int Start => _start;
    public int End => _end;
    public int Length => (_end - _start) < 0 ? (_end - _start) * -1 : (_end - _start);

    public IntRange(int start, int end)
    {
        _start = start;
        _end = end;
        for (int i = start; i <= end; i++)
        {
            this.Append(i);
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        for (int i = _start; i <= _end; i++)
        {
            yield return i;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
public class UIntRange : IEnumerable<uint>
{
    private readonly uint _start;
    private readonly uint _end;
    public uint Start => _start;
    public uint End => _end;
    public long Length => (_end - _start) < 0 ? (_end - _start) * -1 : (_end - _start);

    public UIntRange(uint start, uint end)
    {
        _start = start;
        _end = end;
        for (uint i = start; i <= end; i++)
        {
            this.Append(i);
        }
    }

    public IEnumerator<uint> GetEnumerator()
    {
        for (uint i = _start; i <= _end; i++)
        {
            yield return i;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
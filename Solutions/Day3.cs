using System.Text.RegularExpressions;
using Utility;

public class Day3 : ISolution
{
    public string Name => "Day 3: Mull It Over";

    public Regex _regex = new Regex(@"mul\((\d+),(\d+)\)");
    public string _input;

    public void Setup()
    {
        _input = Fs.GetInput(3);
    }

    public void Part1()
    {
        var result = _regex.Matches(_input);
        int total = 0;
        foreach(Match match in result)
        {
            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);
            total += a * b;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }

    public void Part2()
    {
    }
}
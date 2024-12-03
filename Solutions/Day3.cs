using System.Text.RegularExpressions;
using Utility;

public class Day3 : ISolution
{
    public string Name => "Day 3: Mull It Over";

    public Regex MulRegex = new Regex(@"mul\((\d+),(\d+)\)");
    public Regex DoRegex = new Regex(@"do\(\)");
    public Regex DontRegex = new Regex(@"don't\(\)");
    public string Input = "";

    public void Setup()
    {
        Input = Fs.GetInput(3);
    }

    public void Part1()
    {
        var result = MulRegex.Matches(Input);
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
        var result = MulRegex.Matches(Input);
        List<Match> dos = new(DoRegex.Matches(Input));
        List<Match> donts = new(DontRegex.Matches(Input));
        List<Match> combined = new(dos);
        combined.AddRange(donts);
        combined = combined.OrderBy(x => x.Index).ToList();
        var spans = new List<(int start, int end)>();
        int start = 0;
        bool isDo = true;
        for(int i = 0; i < combined.Count; i++)
        {
            if(dos.Contains(combined[i]) && !isDo)
            {
                isDo = true;
                start = combined[i].Index + combined[i].Length;
                
            }
            else if(donts.Contains(combined[i]) && isDo)
            {
                isDo = false;
                spans.Add((start, combined[i].Index));
            }
            if(i == combined.Count - 1 && isDo)
                spans.Add((start, Input.Length));
        }
        int total = 0;
        foreach(Match match in result)
        {
            if(spans.Any(x => Maths.WithinInclusive(match.Index, x.start, x.end)))
            {
                int a = int.Parse(match.Groups[1].Value);
                int b = int.Parse(match.Groups[2].Value);
                total += a * b;
            }
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
}
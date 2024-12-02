using Utility;

public class Day2 : ISolution
{
    public string Name => "Day 2: Red-Nosed Reports";
    
    private List<List<int>> _data = new();
    public void Setup()
    {
        _data = [];
        string[] data = Fs.GetInput(2).Split("\n");
        foreach(string line in data)
        {
            List<int> list = new();
            line.Split(" ").ToList().ForEach(x => list.Add(int.Parse(x)));
            _data.Add(list);
        }
    }
    public void Part1()
    {
        int safeReports = 0;
        foreach(List<int> list in _data)
        {
            if(IsSafe(list).isSafe) safeReports++;
        }
        ConsoleEx.WriteLineColor(("\nSafe reports: ", ConsoleColor.White), ($"{safeReports}\n", ConsoleColor.Green));
    }
    public void Part2()
    {
        int safeReports = 0;
        foreach(List<int> list in _data)
        {
            int sr = safeReports;
            var result = IsSafe(list);
            if(!result.isSafe)
            {
                List<int> copy1 = new List<int>(list);
                List<int> copy2 = new List<int>(list);
                List<int> copy3 = new List<int>(list);
                List<int> copy4 = new List<int>(list);
                copy1.RemoveAt(result.failIndex);
                var result1 = IsSafe(copy1);
                if(result1.isSafe)
                    safeReports++;
                else if(copy2.Count - 1 >= result1.failIndex + 2)
                {
                    copy2.RemoveAt(result1.failIndex - 1);
                    var result2 = IsSafe(copy2);
                    if(result2.isSafe)
                        safeReports++;
                    else if(result2.failIndex == 2 || result2.failIndex == 1)
                    {
                        copy3.RemoveAt(0);
                        var result3 = IsSafe(copy3);
                        if(result3.isSafe)
                            safeReports++;
                    }
                }
                
            }
            else safeReports++;
        }
        ConsoleEx.WriteLineColor(("\nSafe reports: ", ConsoleColor.White), ($"{safeReports}\n", ConsoleColor.Green));
    }

    public (bool isSafe, int failIndex) IsSafe(List<int> list)
    {
        Direction direction = Direction.None;
        for(int i = 0; i < list.Count; i++)
        {
            if(i == 0) 
            {
                direction = list[i] > list[i+1] ? Direction.Decreasing
                : list[i] < list[i+1] ? Direction.Increasing : Direction.Same;
                if(direction == Direction.Same) 
                    return (false, i+1);
            }
            if(i < list.Count - 1)
            {
                if(list[i] > list[i+1] && direction != Direction.Decreasing || !Maths.WithinInclusive(Math.Abs(list[i] - list[i+1]), 1, 3))
                    return (false, i+1);
                else if(list[i] < list[i+1] && direction != Direction.Increasing || !Maths.WithinInclusive(Math.Abs(list[i] - list[i+1]), 1, 3))
                    return (false, i+1);
                else if(list[i] == list[i+1])
                    return (false, i+1);
            }
            else
            {
                return (true, -1);
            }
        }
        // How the fuck did this get called
        return (false, -1);
    }

    enum Direction
    {
        None,
        Increasing,
        Decreasing,
        Same
    }
}
using Utility;

public class Day2 : ISolution
{
    public string Name => "Day 2: Red-Nosed Reports";
    
    private List<List<int>> _data = new();
    public void Setup()
    {
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
            Direction direction = Direction.None;
            for(int i = 0; i < list.Count; i++)
            {
                if(i == 0) 
                {
                    direction = list[i] > list[i+1] ? Direction.Decreasing
                    : list[i] < list[i+1] ? Direction.Increasing : Direction.Same;
                    if(direction == Direction.Same) break;
                }
                if(i < list.Count - 1)
                {
                    if(list[i] > list[i+1] && direction != Direction.Decreasing || !Maths.WithinInclusive(Math.Abs(list[i] - list[i+1]), 1, 3))
                        break;
                    if(list[i] < list[i+1] && direction != Direction.Increasing || !Maths.WithinInclusive(Math.Abs(list[i] - list[i+1]), 1, 3))
                        break;
                    if(list[i] == list[i+1])
                        break;
                }
                else
                    safeReports++;
            }
        }
        ConsoleEx.WriteLineColor(("\nSafe reports: ", ConsoleColor.White), ($"{safeReports}\n", ConsoleColor.Green));
    }
    public void Part2()
    {
        
    }

    enum Direction
    {
        None,
        Increasing,
        Decreasing,
        Same
    }
}
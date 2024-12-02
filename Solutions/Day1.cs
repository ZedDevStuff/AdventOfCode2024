using Utility;

public class Day1 : ISolution
{
    public string Name => "Day 1: Historian Hysteria";
    private string[] _data = [];
    List<int> _list1 = new(), _list2 = new();
    public void Setup()
    {
        _data = Fs.GetInput(1).Split("\n");
        foreach (var line in _data)
        {
            string[] split = line.Split("   ");
            _list1.Add(int.Parse(split[0]));
            _list2.Add(int.Parse(split[1]));
        }
        _list1.Sort();
        _list2.Sort();
    }
    public void Part1()
    {
        int total = 0;
        for (int i = 0; i < _list1.Count; i++)
        {
            total += Math.Abs(_list1[i] - _list2[i]);
        }
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),($"{total}\n",ConsoleColor.Green));
    }

    public void Part2()
    {
        Dictionary<int, int> dict = new();
        int total = 0;
        for (int i = 0; i < _list1.Count; i++)
        {
            dict[_list1[i]] = _list2.FindAll(x => x == _list1[i]).Count;
            total += Math.Abs(_list1[i] * dict[_list1[i]]);
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
}
using Utility;

public class Day1 : ISolution
{
    public string Name => "Day 1: Historian Hysteria";

    public void Part1()
    {
        string[] data = Fs.GetInput(1).Split("\n");
        List<int> list1 = new(), list2 = new();
        foreach (var line in data)
        {
            string[] split = line.Split("   ");
            list1.Add(int.Parse(split[0]));
            list2.Add(int.Parse(split[1]));
        }
        list1.Sort();
        list2.Sort();
        int total = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            total += Math.Abs(list1[i] - list2[i]);
        }
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),($"{total}\n",ConsoleColor.Green));
    }

    public void Part2()
    {
        string[] data = Fs.GetInput(1).Split("\n");
        List<int> list1 = new(), list2 = new();
        Dictionary<int, int> dict = new();
        foreach (var line in data)
        {
            string[] split = line.Split("   ");
            list1.Add(int.Parse(split[0]));
            list2.Add(int.Parse(split[1]));
        }
        list1.Sort();
        list2.Sort();
        int total = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            dict[list1[i]] = list2.FindAll(x => x == list1[i]).Count;
            total += Math.Abs(list1[i] * dict[list1[i]]);
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
}
using AdventOfCode2024.Extensions;
using AdventOfCode2024.Utility;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Solutions;

public class Day5 : ISolution
{
    public string Name => "Day 5: Print Queue";
    public Dictionary<int, List<int>> Rules;
    public List<int> Order;
    public List<List<int>> Queues;
    public void Setup()
    {
        Rules = new();
        Order = new();
        Queues = new();
        List<string> lines = Fs.GetTest(5).Split('\n').ToList();
        for (int i = 0; i < lines.Count; i++)
        {
            if(char.IsWhiteSpace(lines[i][0])) break;
            string[] parts = lines[i].Split("|");
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            if(Rules.ContainsKey(a))
            {
                if(Rules[a] == null)
                    Rules[a] = new();
                Rules[a].Add(b);
            }
            else
                Rules.Add(a, new() { b });
            //if(!Order.Contains(a)) Order.Add(a);
            //if(!Order.Contains(b)) Order.Add(b);
        }
        /*int s = lines.IndexOf("\r");
        for (int i = s + 1; i < lines.Count; i++)
        {
            Queues.Add(lines[i].Split(",").Select(int.Parse).ToList());
        }
        List<(int place, int page)> ordered = new(Order.Count);
        foreach(var page in new List<int>(Order))
        {
            int place = Order.Count - 1;
            foreach(var rule in Rules)
            {
                if(rule.Value.Contains(page))
                {
                    place--;
                }
            }
            ConsoleEx.WriteLineColor($"{place} ", ConsoleColor.Cyan);
            ordered.Add((place, page));
        }
        ordered.Sort((a, b) => b.place.CompareTo(a.place));
        Order = ordered.Select(x => x.page).ToList();*/
    }
    public void Part1()
    {
        int total = 0;
        int current = 0;
        foreach (List<int> queue in Queues)
        {
            
            // var list = Order.Where(queue.Contains).ToList();
            // //ConsoleEx.WriteLineColor(("Queue: ", ConsoleColor.White), ($"{current}\n", ConsoleColor.Yellow), ("Lists: " + string.Join(", ", queue), ConsoleColor.Green), ("\nLists: " + string.Join(", ", list), ConsoleColor.Cyan));
            // if(list.Count != queue.Count)
            //     continue;
            // else
            // {
            //     for(int i = 0;i < queue.Count;i++)
            //     {
            //         if(list[i] != queue[i])
            //             break;
            //         if(i == queue.Count - 1)
            //             total += queue[queue.Count/2];
            //     }
            // }
            // current++;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public void Part2()
    {

    }

    public bool CanABeBeforeB(int a, int b)
    {
        return false;
    }
}
using AdventOfCode2024.Extensions;
using AdventOfCode2024.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Solutions;

public class Day4 : ISolution
{
    public string Name => "Day 4: Ceres Search";
    public char[,] Chars;
    public void Setup()
    {
        string[] lines = Fs.GetInput(4).Split('\n');
        Chars = new char[lines.Length, lines.Length];
        for (int y = 0; y < lines.Length; y++)
            for(int x = 0; x < lines.Length; x++)
                Chars[x, y] = lines[y][x];
    }
    public void Part1()
    {
        int total = 0;
        for (int y = 0; y < Chars.GetLength(1); y++)
        {
            for (int x = 0; x < Chars.GetLength(0); x++)
            {
                if (Chars[x, y] != 'X') continue;
                List<Direction> directions = CheckSurroundings(x, y, 'M');
                foreach (var direction in directions)
                {
                    if (CheckWordAt(x, y, "XMAS", direction))
                    {
                        //ConsoleEx.WriteLineColor(("Found \"XMAS\" at: ", ConsoleColor.White), ($"({x}, {y})\n", ConsoleColor.Green));
                        total++;
                    }
                }
            }
        }
        ConsoleEx.WriteLineColor(("\nFound \"XMAS\" a total of: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public void Part2()
    {
        int total = 0;
        for (int y = 0; y < Chars.GetLength(1); y++)
        {
            for (int x = 0; x < Chars.GetLength(0); x++)
            {
                if (Chars[x, y] != 'A') continue;
                var directions = CheckSurroundings(x, y, 'M').Where(IsDiagonal).ToDictionary(x => x, x => 'M');
                CheckSurroundings(x, y, 'S').Where(IsDiagonal).ToDictionary(x => x, x => 'S').ForEach(x => directions.Add(x.Key, x.Value));
                if (directions.Count != 4) continue;
                if (directions[Direction.UpLeft] != directions[Direction.DownRight] && directions[Direction.DownLeft] != directions[Direction.UpRight])
                    total++;
            }
        }
        ConsoleEx.WriteLineColor(("\nFound \"XMAS\" a total of: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public List<Direction> CheckSurroundings(int x, int y, char c)
    {
        List<Direction> directions = new();
        if (x - 1 >= 0 && Chars[x - 1, y] == c) directions.Add(Direction.Left);
        if (x + 1 < Chars.GetLength(0) && Chars[x + 1, y] == c) directions.Add(Direction.Right);
        if (y - 1 >= 0 && Chars[x, y - 1] == c) directions.Add(Direction.Up);
        if (y + 1 < Chars.GetLength(1) && Chars[x, y + 1] == c) directions.Add(Direction.Down);
        if (x - 1 >= 0 && y - 1 >= 0 && Chars[x - 1, y - 1] == c) directions.Add(Direction.UpLeft);
        if (x + 1 < Chars.GetLength(0) && y - 1 >= 0 && Chars[x + 1, y - 1] == c) directions.Add(Direction.UpRight);
        if (x - 1 >= 0 && y + 1 < Chars.GetLength(1) && Chars[x - 1, y + 1] == c) directions.Add(Direction.DownLeft);
        if (x + 1 < Chars.GetLength(0) && y + 1 < Chars.GetLength(1) && Chars[x + 1, y + 1] == c) directions.Add(Direction.DownRight);
        return directions;
    }
    public bool CanFit(int x, int y, int length, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return y - length + 1 >= 0;
            case Direction.Down:
                return y + length - 1 < Chars.GetLength(1);
            case Direction.Left:
                return x - length + 1 >= 0;
            case Direction.Right:
                return x + length - 1 < Chars.GetLength(0);
            case Direction.UpLeft:
                return y - length + 1 >= 0 && x - length + 1 >= 0;
            case Direction.UpRight:
                return y - length + 1 >= 0 && x + length - 1 < Chars.GetLength(0);
            case Direction.DownLeft:
                return y + length - 1 < Chars.GetLength(1) && x - length + 1 >= 0;
            case Direction.DownRight:
                return y + length - 1 < Chars.GetLength(1) && x + length - 1 < Chars.GetLength(0);
            default:
                return false;
        }
    }
    public bool CheckWordAt(int x, int y, string word, Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                if (!CanFit(x, y, word.Length, Direction.Up)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x, y - i] != word[i]) return false;
                return true;
            case Direction.Down:
                if (!CanFit(x, y, word.Length, Direction.Down)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x, y + i] != word[i]) return false;
                return true;
            case Direction.Left:
                if (!CanFit(x, y, word.Length, Direction.Left)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x - i, y] != word[i]) return false;
                return true;
            case Direction.Right:
                if (!CanFit(x, y, word.Length, Direction.Right)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x + i, y] != word[i]) return false;
                return true;
            case Direction.UpLeft:
                if(!CanFit(x, y, word.Length, Direction.UpLeft)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x - i, y - i] != word[i]) return false;
                return true;
            case Direction.UpRight:
                if (!CanFit(x, y, word.Length, Direction.UpRight)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x + i, y - i] != word[i]) return false;
                return true;
            case Direction.DownLeft:
                if (!CanFit(x, y, word.Length, Direction.DownLeft)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x - i, y + i] != word[i]) return false;
                return true;
            case Direction.DownRight:
                if (!CanFit(x, y, word.Length, Direction.DownRight)) return false;
                for (int i = 0; i < word.Length; i++)
                    if (Chars[x + i, y + i] != word[i]) return false;
                return true;
            default:
                return false;
        }
    }
    public Direction GetRevese(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.UpLeft => Direction.DownRight,
            Direction.UpRight => Direction.DownLeft,
            Direction.DownLeft => Direction.UpRight,
            Direction.DownRight => Direction.UpLeft,
        };
    }
    public bool IsDiagonal(Direction direction)
    {
        return direction switch
        {
            Direction.UpLeft => true,
            Direction.UpRight => true,
            Direction.DownLeft => true,
            Direction.DownRight => true,
            _ => false
        };
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
}


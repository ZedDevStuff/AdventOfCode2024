using System.Diagnostics;
using AdventOfCode2024;
using AdventOfCode2024.Utility;

public class Day6 : ISolution
{
    public string Name => "Day 6: Guard Gallivant";
    public byte[,] Map, Walked;
    public (int x, int y) Start;
    public void Setup()
    {
        string[] lines = Fs.GetInput(6).Split('\n');
        if(lines.Length == 0)
        {
            ConsoleEx.WriteLineColor(("Error: ", ConsoleColor.Red), ("No data found", ConsoleColor.White));
            return;
        }
        Map = new byte[lines[0].Length - 1, lines.Length];
        Walked = new byte[lines[0].Length - 1, lines.Length];
        for (int x = 0; x < lines[0].Length - 1; x++)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                if(lines[y][x] == '^') Start = (x, y);
                Map[x, y] = (byte)(lines[y][x] == '#' ? 1 : 0);
            }
        }
    }
    public void Part1()
    {
        var total = 0;
        var direction = Direction.North;
        total += RaycastCardinal(Start.x, Start.y, direction);
        var pos = Move(Start.x, Start.y, direction, Math.Abs(total));
        if(Walked[Start.x, Start.y] == 0) Walked[Start.x, Start.y] = 1;
        direction = RotateClockwise(direction);
        if(total > 0)
        {
            while(true)
            {
                int count = RaycastCardinal(pos.x, pos.y, direction);
                if(count < 0) 
                {
                    Move(pos.x, pos.y, direction, Math.Abs(count));
                    break;
                }
                else
                {
                    if(Walked[pos.x, pos.y] == 0) Walked[pos.x, pos.y] = 1;
                    pos = Move(pos.x, pos.y, direction, count);
                    direction = RotateClockwise(direction);
                }
            }
            total = 0;
            foreach (var item in Walked)
            {
                if(item == 1) total++;
            }
        }
        else total = -total + 1;
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public void Part2()
    {
        throw new System.NotImplementedException();
    }

    public void DrawCurrent()
    {
        for(int y = 0; y < Map.GetLength(1); y++)
        {
            for(int x = 0; x < Map.GetLength(0); x++)
            {
                if(Map[x,y] == 1) Console.Write("#");
                else Console.Write(Walked[x,y] == 1 ? "X" : Walked[x,y] == 2 ? "-" : ".");
            }
            Console.WriteLine();
        }
    }

    public int RaycastCardinal(int x, int y, Direction dir)
    {
        int count = 0;
        while (true)
        {
            switch (dir)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                case Direction.East:
                    x++;
                    break;
            }
            if (!IsOutside(x, y))
            {
                if (Map[x, y] == 0)
                    count++;
                else
                    break;
            }
            else 
            {
                count = -count;
                break;
            }

        }
        return count;
    }

    public bool IsOutside(int x, int y)
    {
        return x < 0 || x > Map.GetLength(0) -1 || y < 0 || y > Map.GetLength(1) -1;
    }

    public (int x, int y) Move(int x, int y, Direction dir, int distance)
    {
        for(int i = 0; i < distance; i++)
        {
            switch (dir)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                case Direction.East:
                    x++;
                    break;
            }
            if(Walked[x, y] == 0)
                Walked[x, y] = 1;
            // else if (Walked[x, y] == 1)
            //     Walked[x, y] = 2;
        }
        //DrawCurrent();
        return (x, y);
    }

    public Direction RotateClockwise(Direction dir)
    {
        return dir switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public class Array2D<T>
    {
        public T[] Array;
        public Array2D(int x, int y)
        {
            Array = new T[x * y];
        }
        public T this[int x, int y]
        {
            get => Array[x + y * x];
            set => Array[x + y * x] = value;
        }
    }
}
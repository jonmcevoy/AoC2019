using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day3
    {
        private readonly List<string> _path1;
        private readonly List<string> _path2;

        private Dictionary<int, bool> _board1;
        private Dictionary<int, bool> _board2;

        private Point _start = new Point(0, 0);
        private Point _min = new Point(0, 0);
        private Point _max = new Point(0, 0);

        public Day3(List<string> path1, List<string> path2)
        {
            _path1 = path1;
            _path2 = path2;

            ExpandBounds(_path1);
            ExpandBounds(_path2);

            ProcessPath(_path1, out _board1);
            ProcessPath(_path2, out _board2);
        }

        private void ExpandBounds(List<string> path)
        {
            Point cursor = new Point(0, 0);

            path.ForEach(c =>
            {
                int distance = int.Parse(c.Substring(1));
                switch(c[0])
                {
                    case 'L':
                        cursor.X -= distance;
                        break;
                    case 'R':
                        cursor.X += distance;
                        break;
                    case 'D':
                        cursor.Y -= distance;
                        break;
                    case 'U':
                        cursor.Y += distance;
                        break;
                    default:
                        throw new Exception();
                }

                _max.X = Math.Max(_max.X, cursor.X);
                _max.Y = Math.Max(_max.Y, cursor.Y);

                if (cursor.X < _min.X)
                {
                    _start.X += _min.X - cursor.X;
                    _min.X = cursor.X;
                }
                
                if (cursor.Y < _min.Y)
                {
                    _start.Y += _min.Y - cursor.Y;
                    _min.Y = cursor.Y;
                }
            });
        }

        private void ProcessPath(List<string> path, out Dictionary<int, bool> outBoard)
        {
            var cursor = new Point(_start.X, _start.Y);
            var board = new Dictionary<int, bool>();
            int width = _max.X - _min.X;

            path.ForEach(c =>
            {
                int distance = int.Parse(c.Substring(1));
                for (int i = 0; i < distance; ++i)
                {
                    switch (c[0])
                    {
                        case 'L':
                            cursor.X -= 1;
                            break;
                        case 'R':
                            cursor.X += 1;
                            break;
                        case 'D':
                            cursor.Y -= 1;
                            break;
                        case 'U':
                            cursor.Y += 1;
                            break;
                        default:
                            throw new Exception();
                    }

                    if (cursor.X < 0 || cursor.Y < 0)
                        throw new Exception();

                    board.TryAdd(cursor.X + cursor.Y * width, true);
                }
            });

            outBoard = board;
        }

        public int FindClosestIntersection()
        {
            int maxDistance = _max.X - _min.X + _max.Y - _min.Y;
            Console.WriteLine($"MaxDistance: {maxDistance}");
            int width = _max.X - _min.X;
            Parallel.For(0, maxDistance, (int distance, ParallelLoopState state) =>
            {
                if (state.ShouldExitCurrentIteration && state.LowestBreakIteration < distance)
                {
                    return;
                }

                for (int x = -distance; x <= distance; ++x)
                {
                    int y = distance - Math.Abs(x);
                    foreach (int sign in new int [] { -1, 1 })
                    {
                        if (x + _start.X < 0 || y * sign + _start.Y < 0)
                            continue;

                        int spot = (x + _start.X) + (y * sign + _start.Y) * width;
                        if (_board1.GetValueOrDefault(spot, false) && _board2.GetValueOrDefault(spot, false))
                        {
                            Console.WriteLine($"Found: {distance}");
                            state.Break();
                            break;
                        }
                    }

                    if (state.ShouldExitCurrentIteration && state.LowestBreakIteration <= distance)
                    {
                        break;
                    }
                }
            });

            return 0;
        }
    }
}

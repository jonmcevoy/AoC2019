using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int totalFuel1 = 0;
                foreach (var line in File.ReadLines("input/1.txt"))
                {
                    int mass = Int32.Parse(line);
                    int fuel = (mass / 3) - 2;
                    if (fuel > 0)
                    {
                        totalFuel1 += fuel;
                    }
                }
                Console.WriteLine($"D1P1 fuel: {totalFuel1}");
            }

            {
                int totalFuel2 = 0;
                foreach (var line in File.ReadLines("input/1.txt"))
                {
                    int mass = Int32.Parse(line);
                    int fuel = (mass / 3) - 2;
                    while (fuel > 0)
                    {
                        totalFuel2 += fuel;
                        fuel = (fuel / 3) - 2;
                    }
                }
                Console.WriteLine($"D1P2 fuel: {totalFuel2}");
            }

            {
                var rawOpCodes = File.ReadAllText("input/2.txt");
                var opCodes = rawOpCodes.Split(",").Select(s => Int32.Parse(s)).ToList();
                opCodes[1] = 12;
                opCodes[2] = 2;
                for (int i = 0; opCodes[i] != 99; i += 4)
                {
                    if (opCodes[i] == 1)
                    {
                        opCodes[opCodes[i + 3]] = opCodes[opCodes[i + 1]] + opCodes[opCodes[i + 2]];
                    }
                    else if (opCodes[i] == 2)
                    {
                        opCodes[opCodes[i + 3]] = opCodes[opCodes[i + 1]] * opCodes[opCodes[i + 2]];
                    }
                    else if (opCodes[i] != 99)
                    {
                        throw new Exception("");
                    }
                }
                Console.WriteLine($"D2P1: {opCodes[0]}");
            }

            {
                var rawOpCodes = File.ReadAllText("input/2.txt");
                var opCodesSource = rawOpCodes.Split(",").Select(s => Int32.Parse(s)).ToList();

                Parallel.ForEach(Enumerable.Range(0, 99), (int noun) =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 99), (int verb) =>
                    {
                        List<int> opCodes = new List<int>();
                        opCodes.AddRange(opCodesSource);
                        
                        opCodes[1] = noun;
                        opCodes[2] = verb;
                        for (int i = 0; opCodes[i] != 99; i += 4)
                        {
                            if (opCodes[i] == 1)
                            {
                                opCodes[opCodes[i + 3]] = opCodes[opCodes[i + 1]] + opCodes[opCodes[i + 2]];
                            }
                            else if (opCodes[i] == 2)
                            {
                                opCodes[opCodes[i + 3]] = opCodes[opCodes[i + 1]] * opCodes[opCodes[i + 2]];
                            }
                            else if (opCodes[i] != 99)
                            {
                                throw new Exception("");
                            }
                        }

                        if (opCodes[0] == 19690720)
                        {
                            Console.WriteLine($"D2P2: {100 * noun + verb}");
                        }
                    });
                });
            }

            {
                var paths = File.ReadAllText("input/3.txt");
                var wires = paths.Split(new String[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split(",").ToList()).ToList();
                if (wires.Count() != 2)
                {
                    throw new Exception();
                }

                var d = new Day3(wires[0], wires[1]);
                d.FindClosestIntersection();
                d.FindLeastSteps();
            }

            {
                int count = 0;
                Parallel.For(246540, 787419, p =>
                {
                    var password = Convert.ToString(p);
                    if (password.Length != 6)
                        throw new Exception();

                    bool bSuccess = false;
                    for (int i = 1; i < 6; ++i)
                    {
                        if (password[i] == password[i-1])
                        {
                            bSuccess = true;
                        }
                        else if (password[i] < password[i-1])
                        {
                            bSuccess = false;
                            break;
                        }
                    }

                    if (bSuccess)
                    {
                        Interlocked.Increment(ref count);
                    }
                });

                Console.WriteLine($"D4P1: {count}");
            }

            {
                int count = 0;
                Parallel.For(246540, 787419, p =>
                {
                    var password = Convert.ToString(p);
                    if (password.Length != 6)
                        throw new Exception();

                    bool bIncrement = true;
                    bool bPair = false;
                    int pairCount = 1;
                    for (int i = 1; i < 6; ++i)
                    {
                        if (password[i] < password[i - 1])
                        {
                            bIncrement = false;
                            break;
                        }

                        if (password[i] == password[i - 1])
                        {
                            ++pairCount;
                        }
                        else
                        {
                            if (pairCount == 2)
                            {
                                bPair = true;
                            }
                            pairCount = 1;
                        }
                    }

                    if (pairCount == 2)
                    {
                        bPair = true;
                    }

                    if (bPair && bIncrement)
                    {
                        Interlocked.Increment(ref count);
                    }
                });

                Console.WriteLine($"D4P2: {count}");
            }

            {
                var paths = File.ReadAllText("input/5.txt");
                var d = new Day5Part1(paths.Split(",").Select(i => int.Parse(i)).ToList());
                int output = d.Execute();
                Console.WriteLine($"D5P1: {output}");
            }

            {
                var paths = File.ReadAllText("input/5.txt");
                var d = new IntcodeComputer(paths.Split(",").Select(i => int.Parse(i)).ToList());
                int output = d.Execute(5);
                Console.WriteLine($"D5P1: {output}");
            }
        }
    }
}

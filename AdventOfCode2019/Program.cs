using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        }
    }
}

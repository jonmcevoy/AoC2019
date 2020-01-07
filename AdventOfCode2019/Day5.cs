using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day5
    {
        private readonly List<int> _storage = new List<int>();

        public Day5(List<int> storage)
        {
            _storage.AddRange(storage);
        }

        private int GetParameter(List<int> storage, int modes, int ip, int position)
        {
            int immediate = storage[ip + position];
            int denominator = 1;
            for (int i = 1; i < position; ++i) denominator *= 10;

            if ((modes / denominator) % 10 == 1)
            {
                return immediate;
            }

            return storage[immediate];
        }

        private void CheckWrite(int modes, int position)
        {
            if ((modes / position) % 10 == 1)
            {
                throw new Exception();
            }
        }

        private void CheckOutput(int output)
        {
            if (output != 0)
            {
                throw new Exception();
            }
        }

        public int Execute()
        {
            List<int> executionStorage = new List<int>();
            executionStorage.AddRange(_storage);

            int ip = 0;
            bool halt = false;
            int output = 0;
            int input = 1;

            while (!halt)
            {
                int instruction = executionStorage[ip] % 100;
                int modes = executionStorage[ip] / 100;
                int destination = -1;
                switch (instruction)
                {
                    case 1:
                        CheckWrite(modes, 3);
                        CheckOutput(output);
                        destination = executionStorage[ip + 3];
                        executionStorage[destination] = GetParameter(executionStorage, modes, ip, 1) + GetParameter(executionStorage, modes, ip, 2);
                        ip += 4;
                        break;
                    case 2:
                        CheckWrite(modes, 3);
                        CheckOutput(output);
                        destination = executionStorage[ip + 3];
                        executionStorage[destination] = GetParameter(executionStorage, modes, ip, 1) * GetParameter(executionStorage, modes, ip, 2);
                        ip += 4;
                        break;
                    case 3:
                        CheckWrite(modes, 1);
                        CheckOutput(output);
                        destination = executionStorage[ip + 1];
                        executionStorage[destination] = input;
                        ip += 2;
                        break;
                    case 4:
                        CheckOutput(output);
                        output = GetParameter(executionStorage, modes, ip, 1);
                        ip += 2;
                        break;
                    case 99:
                        halt = true;
                        break;
                    default:
                        throw new Exception();
                }
            }

            return output;
        }
    }
}

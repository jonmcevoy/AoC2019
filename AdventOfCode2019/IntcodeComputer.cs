using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class IntcodeComputer
    {
        private readonly List<int> _storage = new List<int>();

        public IntcodeComputer(List<int> storage)
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

        public int Execute(int input)
        {
            List<int> executionStorage = new List<int>();
            executionStorage.AddRange(_storage);

            int ip = 0;
            bool halt = false;
            int output = 0;

            while (!halt)
            {
                int instruction = executionStorage[ip] % 100;
                int modes = executionStorage[ip] / 100;
                int destination = -1;

                if (instruction != 99)
                {
                    CheckOutput(output);
                }

                switch (instruction)
                {
                    case 1:
                        CheckWrite(modes, 3);
                        destination = executionStorage[ip + 3];
                        executionStorage[destination] = GetParameter(executionStorage, modes, ip, 1) + GetParameter(executionStorage, modes, ip, 2);
                        ip += 4;
                        break;
                    case 2:
                        CheckWrite(modes, 3);
                        destination = executionStorage[ip + 3];
                        executionStorage[destination] = GetParameter(executionStorage, modes, ip, 1) * GetParameter(executionStorage, modes, ip, 2);
                        ip += 4;
                        break;
                    case 3:
                        CheckWrite(modes, 1);
                        destination = executionStorage[ip + 1];
                        executionStorage[destination] = input;
                        ip += 2;
                        break;
                    case 4:
                        output = GetParameter(executionStorage, modes, ip, 1);
                        ip += 2;
                        break;
                    case 5:
                        {
                            int conditional = GetParameter(executionStorage, modes, ip, 1);
                            if (conditional != 0)
                            {
                                ip = GetParameter(executionStorage, modes, ip, 2);
                            }
                            else
                            {
                                ip += 3;
                            }
                        }
                        break;
                    case 6:
                        {
                            int conditional = GetParameter(executionStorage, modes, ip, 1);
                            if (conditional == 0)
                            {
                                ip = GetParameter(executionStorage, modes, ip, 2);
                            }
                            else
                            {
                                ip += 3;
                            }
                        }
                        break;
                    case 7:
                        CheckWrite(modes, 3);
                        {
                            int toStore = 0;
                            if (GetParameter(executionStorage, modes, ip, 1) < GetParameter(executionStorage, modes, ip, 2))
                            {
                                toStore = 1;
                            }

                            destination = executionStorage[ip + 3];
                            executionStorage[destination] = toStore;
                        }
                        ip += 4;
                        break;
                    case 8:
                        CheckWrite(modes, 3);
                        {
                            int toStore = 0;
                            if (GetParameter(executionStorage, modes, ip, 1) == GetParameter(executionStorage, modes, ip, 2))
                            {
                                toStore = 1;
                            }

                            destination = executionStorage[ip + 3];
                            executionStorage[destination] = toStore;
                        }
                        ip += 4;
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

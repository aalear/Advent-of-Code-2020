using System;
using System.IO;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Part 1: " + PartOne(input));
            Console.WriteLine("Part 2: " + PartTwo(input));
        }

        static int PartOne(string[] input)
        {
            TryDetectLoop(input, out int accumulator);
            return accumulator;
        }

        static int PartTwo(string[] input)
        {
            var from = "";
            var to = "";

            for(var i = 0; i < input.Length; i++)
            {
                (string op, bool isPositive, int arg) = ParseLine(input[i]);

                switch(op) {
                    case "jmp":
                        from = "jmp";
                        to = "nop";
                        break;
                    case "nop":
                        from = "nop";
                        to = "jmp";
                        break;
                    default:
                        continue;
                }

                input[i] = input[i].Replace(from, to);

                if(TryDetectLoop(input, out int accumulator))
                {
                    input[i] = input[i].Replace(to, from);
                }
                else
                {
                    return accumulator;
                }
            }

            // will never hit this given the constraints of the puzzle
            return int.MinValue; 
        }

        static void CopyArray(string[] src, string[] dest) => Array.Copy(src, dest, src.Length);

        static bool TryDetectLoop(string[] input, out int accumulator)
        {
            accumulator = 0;
            var executed = new bool[input.Length];

            var i = 0; // instruction index
            while(true) 
            {
                (string op, bool isPositive, int arg) = ParseLine(input[i]);

                if(executed[i])
                {
                    return true;
                }
                else
                {
                    executed[i] = true;
                }

                if(op == "acc")
                {
                    accumulator = isPositive ? accumulator + arg : accumulator - arg;
                    i++;
                }
                else if(op == "jmp")
                {
                    i = isPositive ? i + arg : i - arg;
                }
                else if(op == "nop")
                {
                    i++;
                }

                if(i >= input.Length) 
                {
                    return false;
                }
            }
        }

        static (string op, bool isPositive, int arg) ParseLine(string line) {
            var tokens = line.Split(" ");
            return (tokens[0], tokens[1].StartsWith("+"), int.Parse(tokens[1].Substring(1)));
        }
    }
}

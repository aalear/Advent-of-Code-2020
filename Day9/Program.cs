using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        private static long[] data;
        const int preambleLength = 25;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            data = new long[input.Length];
            for(var i = 0; i < input.Length; i++)
            {
                data[i] = long.Parse(input[i]);

                if(i >= preambleLength)
                {
                    if(!TryFindSum(i))
                    {
                        Console.WriteLine("Part 1: " + data[i]);

                        var weakness = FindWeakness(i);
                        Console.WriteLine("Part 2: " + (weakness.Min + weakness.Max));

                        break;
                    }
                }
            }
        }

        static bool TryFindSum(int index)
        {
            var desiredSum = data[index];

            for(var i = index - preambleLength; i < index - 1; i++) 
            {
                for(var j = i + 1; j < index; j++)
                {
                    if(data[i] + data[j] == desiredSum) 
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static (long Min, long Max) FindWeakness(int index)
        {
            var desiredSum = data[index];

            for(var i = 0; i < index - 1; i++) 
            {
                var sequence = new List<long> { data[i] };
                for(var j = i + 1; j < index; j++)
                {
                    sequence.Add(data[j]);

                    var sum = sequence.Sum();
                    if(sum == desiredSum)
                    {
                        return (sequence.Min(), sequence.Max());
                    }
                    else if(sum > desiredSum)
                    {
                        break;
                    }
                }
            }

            throw new InvalidDataException("All the data is bad and guaranteed to have a weakness");
        }
    }
}

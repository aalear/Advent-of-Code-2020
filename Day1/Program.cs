using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var numbers = new List<int>();
            foreach(var line in input) 
            {
                numbers.Add(int.Parse(line));
            }

            Console.WriteLine("Part 1: " + PartOne(numbers));
            Console.WriteLine("Part 2: " + PartTwo(numbers));
        }

        private static int PartOne(List<int> input) 
        {
            for(var i = 0; i < input.Count; i++) 
            {
                var n1 = input[i];
                for(var j = i + 1; j < input.Count; j++)
                {
                    var sum = n1 + input[j];
                    if(sum == 2020) {
                        return n1 * input[j];
                    }
                }
            }
            return 0;
        }

        private static int PartTwo(List<int> input) 
        {
            for(var i = 0; i < input.Count; i++) 
            {
                var n1 = input[i];
                for(var j = i + 1; j < input.Count; j++)
                {
                    var n2 = input[j];
                    for(var k = j + 1; k < input.Count; k++) 
                    {
                        if(n1 + n2 + input[k] == 2020) {
                            return n1 * n2 * input[k];
                        }
                    }
                }
            }
            return 0;
        }
    }
}

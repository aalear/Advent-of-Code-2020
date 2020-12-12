using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var adapters = input.Select(i => int.Parse(i)).OrderBy(i => i).ToList();
            adapters.Add(adapters.Last() + 3);

            var diffCount = new Dictionary<int, int> {
                { 1, 0 },
                { 2, 0 },
                { 3, 0}
            };
            diffCount[adapters[0]]++;

            for(var i = 1; i < adapters.Count; i++)
            {
                var diff = adapters[i] - adapters[i - 1];
                diffCount[diff]++;
            }

            Console.WriteLine("Part 1: " + (diffCount[1] * diffCount[3]));
        }
    }
}

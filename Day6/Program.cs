using System;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var sumAny = 0;
            var sumAll = 0;
            bool[] questionsAny = null;
            int[] questionsAll = null;
            var groupSize = 0;
            var newGroup = true;
            foreach(var line in input) 
            {
                if(string.IsNullOrEmpty(line))
                {
                    sumAny += questionsAny.Count(q => q);
                    sumAll += questionsAll.Count(q => q == groupSize);

                    newGroup = true;
                    continue;
                }

                if(newGroup)
                {
                    questionsAny = new bool[26];
                    questionsAll = new int[26];
                    groupSize = 0;
                    newGroup = false;
                }

                foreach(var q in line) 
                {
                    questionsAny[q - 97] = true;
                    questionsAll[q - 97]++;
                }
                groupSize++;
            }
            sumAny += questionsAny.Count(q => q);
            sumAll += questionsAll.Count(q => q == groupSize);

            Console.WriteLine("Part 1: " + sumAny);
            Console.WriteLine("Part 2: " + sumAll);
        }
    }
}

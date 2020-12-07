using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var patternWidth = input[0].Length;
            var mapHeight = input.Length;
            // false = open, true = tree
            var pattern = new bool[mapHeight, patternWidth];

            for(var i = 0; i < mapHeight; i++)
            {
                var line = input[i];
                for(var j = 0; j < patternWidth; j++)
                {
                    if(line[j] == '#')
                    {
                        pattern[i, j] = true;
                    }
                }
            }

            var slope31 = SlopeCount(pattern, patternWidth, mapHeight, 3, 1);
            Console.WriteLine("Part 1: " + slope31);

            var slope11 = SlopeCount(pattern, patternWidth, mapHeight, 1, 1);
            var slope51 = SlopeCount(pattern, patternWidth, mapHeight, 5, 1);
            var slope71 = SlopeCount(pattern, patternWidth, mapHeight, 7, 1);
            var slope12 = SlopeCount(pattern, patternWidth, mapHeight, 1, 2);

            var result = (long)slope11 * (long)slope31 * (long)slope51 * (long)slope71 * (long)slope12;

            Console.WriteLine($"Part 2: {result}");
        }

        static int SlopeCount(bool[,] pattern, int width, int height, int widthStep, int heightStep)
        {
            var col = widthStep;
            var treeCount = 0;

            for(var row = heightStep; row < height; row += heightStep)
            {
                if(pattern[row, col])
                {
                    treeCount++;
                }
                col += widthStep;
                col %= width;
            }

            return treeCount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var seatIds = new List<int>();
            foreach(var line in input)
            {
                var minRow = 0;
                var maxRow = 127;

                var chosenRow = -1;
                var chosenSeat = -1;

                var rowSpec = line.Substring(0, 6);
                var colSpec = line.Substring(7, 2);
                foreach(var c in rowSpec)
                {
                    if(c == 'F') 
                    {
                        maxRow = (maxRow - minRow) / 2 + minRow;
                    }
                    else if(c == 'B')
                    {
                        minRow = (maxRow - minRow) / 2 + 1 + minRow;
                    }
                }
                var finalRowSpec = line[6];
                chosenRow = finalRowSpec == 'F' ? minRow : maxRow;

                var minCol = 0;
                var maxCol = 7;
                foreach(var c in colSpec)
                {
                    if(c == 'L') 
                    {
                        maxCol = (maxCol - minCol) / 2 + minCol;
                    }
                    else if(c == 'R')
                    {
                        minCol = (maxCol - minCol) / 2 + 1 + minCol;
                    }
                }
                var finalSeatSpec = line[9];
                chosenSeat = finalSeatSpec == 'L' ? minCol : maxCol;

                seatIds.Add(chosenRow * 8 + chosenSeat);
            }

            Console.WriteLine("Part 1: " + seatIds.Max());

            seatIds = seatIds.OrderBy(id => id).ToList();
            var mySeat = 0;
            for(var i = 1; i < seatIds.Count - 1; i++)
            {
                var seat = seatIds[i];
                var prevSeat = seatIds[i - 1];
                var nextSeat = seatIds[i + 1];

                if(seat - prevSeat > 1)
                {
                    mySeat = prevSeat + 1;
                    break;
                }
                else if(nextSeat - seat > 1)
                {
                    mySeat = nextSeat - 1;
                    break;
                }
            }
            Console.WriteLine("Part 2: " + mySeat);
        }
    }
}

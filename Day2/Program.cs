using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            
            var passwords = new List<Password>();
            foreach(var line in input) 
            {
                var tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if(tokens.Length != 3) 
                {
                    throw new InvalidDataException();
                }

                var dashIdx = tokens[0].IndexOf('-');
                passwords.Add(new Password 
                {
                    FirstConstraint = int.Parse(tokens[0].Substring(0, dashIdx)),
                    SecondConstraint = int.Parse(tokens[0].Substring(dashIdx + 1)),
                    RequiredLetter = Convert.ToChar(tokens[1].Replace(":", "")),
                    Text = tokens[2]
                });
            }

            Console.WriteLine("Part 1: " + PartOne(passwords));
            Console.WriteLine("Part 2: " + PartTwo(passwords));
        }

        static int PartOne(List<Password> passwords) => passwords.Where(p => p.IsValidOne()).Count();
        static int PartTwo(List<Password> passwords) => passwords.Where(p => p.IsValidTwo()).Count();
    }

    class Password
    {
        public char RequiredLetter { get; set; }

        // Part 1: min number of occurrences
        // Part 2: first position index in the password
        public int FirstConstraint { get; set; }

        // Part 1: max number of occurrences
        // Part 2: second position index in the password
        public int SecondConstraint { get; set; }
        public string Text { get; set; }

        public bool IsValidOne() 
        {
            var isValid = false;
            var count = 0;
            foreach(var c in Text)
            {
                if(c != RequiredLetter)
                    continue;

                count++;
                isValid = count >= FirstConstraint && count <= SecondConstraint;
            }

            return isValid;
        }

        public bool IsValidTwo()  {
            // Adjust for one-based indexing
            var i = FirstConstraint - 1;
            var j = SecondConstraint - 1;

            return (Text[i] == RequiredLetter && Text[j] != RequiredLetter)
                || (Text[i] != RequiredLetter && Text[j] == RequiredLetter);
        } 
    }
}

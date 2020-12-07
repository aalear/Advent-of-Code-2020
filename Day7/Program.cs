using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Program
    {
        static Regex innerBagRegex = new Regex(@"(?<count>\d+) (?<type>\w+ \w+) bags?\.?");

        internal static Dictionary<string, Bag> AllBags = new Dictionary<string, Bag>();

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            foreach(var line in input)
            {
                var tokens = line.Split(" contain ");

                var bagType = tokens[0].Replace(" bags", "");

                Bag bag;
                if(AllBags.ContainsKey(bagType))
                {
                    bag = AllBags[bagType];
                }
                else
                {
                    bag = new Bag { Type = bagType };
                    AllBags.Add(bagType, bag);
                }

                if(tokens[1] == "no other bags")
                {
                    continue;
                }
                else
                {
                    var innerTokens = tokens[1].Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    foreach(var b in innerTokens)
                    {
                        var parsed = innerBagRegex.Match(b);
                        if(parsed.Success)
                        {
                            var type = parsed.Groups[2].Value;
                            bag.InnerBagCountPerType[type] = int.Parse(parsed.Groups[1].Value);

                            if(!AllBags.ContainsKey(type))
                            {
                                AllBags.Add(type, new Bag { Type = type });
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Part 1: " + AllBags.Values.Where(b => b.CanCarryShinyGold()).Count());
            Console.WriteLine("Part 2: " + AllBags["shiny gold"].CountInner());
        }
    }

    class Bag
    {
        internal string Type { get; set; }
        internal Dictionary<string, int> InnerBagCountPerType { get; } = new Dictionary<string, int>();

        internal bool CanCarryShinyGold()
        {
            if(InnerBagCountPerType.Count == 0)
            {
                return false;
            }

            var canCarry = false;
            foreach(var b in InnerBagCountPerType.Keys)
            {
                if(b == "shiny gold")
                {
                    return true;
                }
                else
                {
                    canCarry = Program.AllBags[b].CanCarryShinyGold();
                    if(canCarry) break;
                }
            }
            return canCarry;
        }

        internal int CountInner()
        {
            var sum = 0;

            if(InnerBagCountPerType.Count == 0)
            {
                return 0;
            }

            foreach(var innerBagType in InnerBagCountPerType.Keys)
            {
                for(var i = 0; i < InnerBagCountPerType[innerBagType]; i++)
                {
                    sum++;
                    sum += Program.AllBags[innerBagType].CountInner();
                }
            }

            return sum;
        }
    }
}

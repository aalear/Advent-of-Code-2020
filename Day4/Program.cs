using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var requiredKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            var passports = new List<Dictionary<string, string>>();
            var validators = SetupValidators();

            var create = true;
            Dictionary<string, string> currentPassport = null;
            foreach(var line in input) 
            {
                if(create)
                {
                    currentPassport = new Dictionary<string, string>();
                    passports.Add(currentPassport);
                    create = false;
                }

                var tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach(var token in tokens)
                {
                    var dataPair = token.Split(":");
                    currentPassport[dataPair[0]] = dataPair[1];
                }

                if(string.IsNullOrEmpty(line))
                {
                    create = true;
                }
            }

            // Part 1 - all fields must be present, except cid
            var allFieldsPresentCount = 0;
            var validCount = 0;
            foreach(var passport in passports)
            {
                if(requiredKeys.All(k => passport.Keys.Contains(k)))
                {
                    allFieldsPresentCount++;

                    var isValid = true;
                    foreach(var field in passport)
                    {
                        var label = field.Key;
                        var data = field.Value;
                        if(!validators[label](data)) {
                            isValid = false;
                            break;
                        }
                    }
                    if(isValid) validCount++;
                }
            }

            Console.WriteLine("Part 1: " + allFieldsPresentCount);
            Console.WriteLine("Part 2: " + validCount);
        }

        static Dictionary<string, Func<string, bool>> SetupValidators() 
        {
            var retval = new Dictionary<string, Func<string, bool>>();

            var validEyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            retval.Add("byr", (v) => int.TryParse(v, out int year) && year >= 1920 && year <= 2002);
            retval.Add("iyr", (v) => int.TryParse(v, out int year) && year >= 2010 && year <= 2020);
            retval.Add("eyr", (v) => int.TryParse(v, out int year) && year >= 2020 && year <= 2030);

            retval.Add("hgt", (v) => {
                var isCm = v.EndsWith("cm");
                var isIn = v.EndsWith("in");

                if(!isCm && !isIn) return false;

                return isCm 
                    ? int.TryParse(v.Replace("cm", ""), out int height) && height >= 150 && height <= 193
                    : int.TryParse(v.Replace("in", ""), out height) && height >= 59 && height <= 76;
            });

            retval.Add("hcl", (v) => {
                if(!v.StartsWith("#"))
                    return false;

                return Regex.IsMatch(v, "^#[0-9a-f]{6}$");
            });

            retval.Add("ecl", (v) => validEyeColors.Contains(v));
            retval.Add("pid", (v) => Regex.IsMatch(v, @"^[0-9]{9}$"));
            retval.Add("cid", (v) => true);

            return retval;
        }
    }
}

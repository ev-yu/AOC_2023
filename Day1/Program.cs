using System.Reflection.Metadata;
using System.Xml.Linq;
using Colibri;

namespace Day1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Part2();
		}

		static void Part1()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int total = 0;
			foreach (string line in lines)
			{

				List<char> nums = line.Where(x => x >= '0' && x <= '9').ToList();
				if (nums.Count == 1)
					nums.Add(nums.Single());
				else
					nums = new List<char>() { nums.First(), nums.Last() };
				string numString = nums.First().ToString() + nums.Last().ToString();
				int value = int.Parse(numString);
				total += value;
			}
			Console.WriteLine(total);
		}

		static void Part2()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int total = 0;
			// Maps number strings to numbers
			Dictionary<string, int> numberMapping = new()
			{
				{ "one", 1 }, { "two", 2 }, { "three", 3 }, 
				{ "four", 4 }, { "five", 5 }, { "six", 6 }, 
				{ "seven", 7 }, { "eight", 8 }, { "nine", 9 }
			};
			foreach (string line in lines)
			{
				// Keep track of all the numbers we found on this line
				List<int> foundNumbers = new();
				int i = 0;
				while (i < line.Length)
				{
					// First, we skip all the characters we've already read
					string substring = line.Substring(i);
					// Here, we select all of the entries in the numberMapping dict which match the start of our substring
					// This will leave exactly one or zero entries, but KeyValuePair isnt a reference type so i can't just use SingleOrNull :(
					KeyValuePair<string, int> writtenNumberMatch = numberMapping.SingleOrDefault(x => substring.StartsWith(x.Key));

					// Check if the first character in the substring is a number character
					char firstChar = substring.First();
					if (firstChar >= '0' && firstChar <= '9')
					{
						// If it is, stop parsing
						i++;
						foundNumbers.Add(int.Parse(firstChar.ToString()));
					}
					// Else, if we matched against a "written" number, use that instead
					else if (writtenNumberMatch.Key != null)
					{
						i += writtenNumberMatch.Key.Length - 1;
						foundNumbers.Add(writtenNumberMatch.Value);
					}
					// Otherwise, just skip to the next one
					else
					{
						i++;
					}
				}
				// Get the first and last found numbers.
				// If only one was found, this will conveniently pick it twice
				string numString = foundNumbers.First().ToString() + foundNumbers.Last().ToString();
				int value = int.Parse(numString);
				total += value;
			}
			Console.WriteLine(total);
		}
	}
}

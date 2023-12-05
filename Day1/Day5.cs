using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Colibri;
using Newtonsoft.Json.Linq;

namespace Solutions
{
	internal class Day5
	{
		class Mapping
		{
			public ulong Start;
			public ulong Length;
			public ulong MapStart;
		}

		public static void Part1()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			List<ulong> unmapped = lines.First().Substring(7).Split(' ').Select(ulong.Parse).ToList();
			List<ulong> mapped = new();
			foreach (string line in lines.Skip(3))
			{
				// If line is empty, skip it
				if (line.Length == 0)
					continue;
				// If line starts with a non-digit instead, copy the mapped values to unmapped to reset the tracking
				if (!char.IsDigit(line[0]))
				{
					mapped.AddRange(unmapped);
					unmapped = mapped.ToList();
					mapped = new();
					continue;
				}

				List<ulong> parts = line.Split(' ').Select(ulong.Parse).ToList();
				ulong inputStart = parts[1];
				ulong inputLength = parts[2];
				ulong outputStart = parts[0];

				// Use ToList here to copy the list in-place
				foreach (ulong seed in unmapped.ToList())
				{
					if (seed >= inputStart && seed < inputStart + inputLength)
					{
						unmapped.Remove(seed);
						mapped.Add(seed - inputStart + outputStart);

					}
				}

			}

			mapped.AddRange(unmapped);
			Console.WriteLine(string.Join(", ", mapped));
			Console.WriteLine(mapped.Min());
		}

		static List<List<Mapping>> mappings = new();
		static List<Mapping> inputMappings = new();

		// I tried to do an analytical solution here...
		// It worked on the example input
		// But not on the real one
		// So i gave up
		// This is heresy, but so it be
		// This doesnt even work anymore, i dont care
		public static void Part2()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();

			foreach (ulong[] vals in lines.First().Substring(7).Split(' ').Select(ulong.Parse).Chunk(2))
			{
				inputMappings.Add(new Mapping() { Start = vals[0], Length = vals[1] });
			}

			List<Mapping> currentCategory = new();
			foreach (string line in lines.Skip(3))
			{
				// If line is empty, skip it
				if (line.Length == 0)
					continue;
				// If line starts with a non-digit instead, create a new category
				if (!char.IsDigit(line[0]))
				{
					mappings.Add(currentCategory);
					currentCategory = new();
					continue;
				}

				List<ulong> parts = line.Split(' ').Select(ulong.Parse).ToList();
				ulong inputStart = parts[1];
				ulong inputLength = parts[2];
				ulong outputStart = parts[0];

				currentCategory.Add(new Mapping() { Start = inputStart, Length = inputLength, MapStart = outputStart});
			}

			//mappings.Reverse();
			List<KeyValuePair<ulong, ulong>> found = new();
			ulong lowest = ulong.MaxValue;
			foreach (Mapping map in inputMappings)
			{
				Console.WriteLine("test");
				lowest = ulong.Min(lowest, Range(map.Start, map.Start + map.Length).AsParallel().Select(Calc).Min());
			}

			ulong test = lowest;
			Console.WriteLine(lowest);
		}

		static ulong Calc(ulong i)
		{
			ulong value = i;
			foreach (List<Mapping> category in mappings)
			{
				foreach (Mapping mapping in category)
				{
					if (value >= mapping.Start && value < mapping.Start + mapping.Length)
					{
						value = value - mapping.Start + mapping.MapStart;
						break;
					}
				}
			}
			return value;
		}

		public static IEnumerable<ulong> Range(ulong source, ulong end)
		{
			for (ulong i = source; i < end; i++)
			{
				yield return i;
			}
		}
	}
}

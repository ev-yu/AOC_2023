using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day6
	{
		public static void Part1()
		{
			List<List<int>> lines = File.ReadAllLines("input.txt").Select(x => 
					x.Substring(11)
					.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToList()
				).ToList();
			ulong total = lines[0].Zip(lines[1]).Select(entry => (ulong)Enumerable.Range(0, entry.First).Count(x => (entry.First - x) * x > entry.Second)).Aggregate((a,b) => a*b);
			Console.WriteLine(total);
		}

		// Who needs a nice solution when you can have an easy solution
		public static void Part2()
		{
			List<List<ulong>> lines = File.ReadAllLines("input.txt").Select(x =>
				x.Substring(11)
					.Replace(" ", "")
					.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(ulong.Parse)
					.ToList()
			).ToList();
			ulong total = lines[0].Zip(lines[1]).Select(entry => (ulong)Enumerable.Range(0, (int)entry.First).Count(x => (entry.First - (ulong)x) * (ulong)x > entry.Second)).Aggregate((a, b) => a * b);
			Console.WriteLine(total);
		}
	}
}

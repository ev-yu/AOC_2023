using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day8
	{
		public static void Part1()
		{

			List<string> lines = File.ReadAllLines("input.txt").ToList();
			List<int> instructions = lines[0].Select(x => x == 'L' ? 0 : 1).ToList();
			Dictionary<string, string[]> nodes = lines.Skip(2).ToDictionary(k => k.Substring(0, 3), v => v.Substring(7, 8).Split(", "));
			int i = 0;
			string nextNode = "AAA";
			while (nextNode != "ZZZ")
			{
				nextNode = nodes[nextNode][instructions[i % instructions.Count]];
				i++;
			}

			Console.WriteLine(i);
		}
		public static void Part2()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			List<int> instructions = lines[0].Select(x => x == 'L' ? 0 : 1).ToList();
			Dictionary<string, string[]> nodes = lines.Skip(2).ToDictionary(k => k.Substring(0, 3), v => v.Substring(7, 8).Split(", "));
			ulong i = 0;
			int x = 0;
			string[] nextNodes = nodes.Keys.Where(n => n[2] == 'A').ToArray();
			List<int> cycleTimes = new()
			while (nextNodes.Any(n => n[2] != 'Z'))
			{
				int side = instructions[x];
				for (int j = 0; j < nextNodes.Length; j++)
				{
					nextNodes[j] = nodes[nextNodes[j]][side];
				}
				i++;
				x++;
				if (x >= instructions.Count)
				{
					x = 0;
				}
			}

			Console.WriteLine(i);
		}

	}
}

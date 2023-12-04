using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day4
	{
		public static void Part1()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int total = 0;
			foreach (string line in lines)
			{
				// For each line in the input, split off the card index, since we don't need it
				List<List<string>> parts = line.Split(": ")
					.Last()
					// Then, split into the left and right halves
					.Split(" | ")
					// Then, for each half, split into a list of numbers
					.Select(x => 
						x.Split(" ").Where(x => x != "").ToList()
						).ToList();
					// Use the intersection of the left and right halves to calculate the 
				int winning = (int)Math.Pow(2, parts.First().Intersect(parts.Last()).Count() - 1);
				total += winning;
			}
			Console.WriteLine(total);
		}

		struct Card
		{
			public int Index;
			public int Value;
		}
		static List<Card> _matchValues = new();
		static Dictionary<int, int> _processedValues = new();
		public static void Part2()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int total = 0;
			int i = 0;
			foreach (string line in lines)
			{
				// For each line in the input, split off the card index, since we don't need it
				List<List<string>> parts = line.Split(": ")
					.Last()
					// Then, split into the left and right halves
					.Split(" | ")
					// Then, for each half, split into a list of numbers
					.Select(x =>
						x.Split(" ").Where(x => x != "").ToList()
					).ToList();
				// Use the intersection of the left and right halves to calculate the 
				int winning = parts.First().Intersect(parts.Last()).Count();
				// We save the index and value here, so that we can later use the index in the ProcessCard function
				_matchValues.Add(new Card() {Index = i + 1, Value = winning});
				i++;
			}

			total = _matchValues.Sum(ProcessCard);

			Console.WriteLine(total);
		}

		static int ProcessCard(Card card)
		{
			if (_processedValues.TryGetValue(card.Index, out int cachedValue))
				return cachedValue;
			// 1 for the card itself, plus every "child" card it won
			int value = 1 + _matchValues.Skip(card.Index).Take(card.Value).Sum(ProcessCard);

			_processedValues[card.Index] = value;
			return value;
		}
	}
}

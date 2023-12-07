using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day7
	{
		enum Value
		{
			Special = 0,
			Five,
			Four,
			FullHouse,
			Three,
			TwoPair,
			Pair,
			High
		}

		class Hand
		{
			public string Original;
			public List<int> Cards;
			public int Bet;

			public Value Value;

			public Hand(List<int> cards, int bet)
			{
				Cards = cards;
				Bet = bet;
			}
		}

		static string _cardOrder = "23456789TJQKA";
		static string _cardOrder2 = "J23456789TQKA";

		public static void Part1()
		{
			List<Hand> hands = File.ReadAllLines("input.txt").Select(hand =>
			{
				string[] parts = hand.Split(' ');
				return new Hand(parts[0].Select(p => _cardOrder.IndexOf(p)).ToList(), 
				int.Parse(parts[1]))
				{
					Original = parts[0]
				};
			}).ToList();

			foreach (Hand hand in hands)
			{
				IGrouping<int, int> largestGrouping = hand.Cards.GroupBy(c => c).MaxBy(g => g.Count())!;
				int groupCount = hand.Cards.GroupBy(ch => ch).Count();
				switch (groupCount)
				{
					// Five of a kind
					case 1:
						hand.Value = Value.Five;
						break;
					// Four of a kind and Full house
					case 2:
						hand.Value = largestGrouping.Count() == 4 ? Value.Four : Value.FullHouse;
						break;
					case 3:
					{
						// Three of a kind and Two pair
						hand.Value = largestGrouping.Count() == 3 ? Value.Three : Value.TwoPair;
						break;
					}
					// One pair
					case 4:
						hand.Value = Value.Pair;
						break;

					// High card
					default:
						hand.Value = Value.High;
						break;
				}
			}
			hands = hands.OrderByDescending(h => h.Value)
				.ThenBy(x => x.Cards[0])
				.ThenBy(x => x.Cards[1])
				.ThenBy(x => x.Cards[2])
				.ThenBy(x => x.Cards[3])
				.ThenBy(x => x.Cards[4])
				.ToList();

			ulong total = 0;
			ulong i = 1;
			foreach (Hand hand in hands)
			{
				total += (ulong)hand.Bet * i;
				Console.WriteLine($"{hand.Original} {(int)hand.Value}");
				i++;
			}

			Console.WriteLine(total);
		}

		public static void Part2()
		{
			List<Hand> hands = File.ReadAllLines("input.txt").Select(hand =>
			{
				string[] parts = hand.Split(' ');
				return new Hand(parts[0].Select(p => _cardOrder2.IndexOf(p)).ToList(),
					int.Parse(parts[1]))
				{
					Original = parts[0]
				};
			}).ToList();

			foreach (Hand hand in hands)
			{
				int jCount = hand.Cards.Count(x => x == 0);
				if (hand.Original == "JJJJJ")
				{
					hand.Value = Value.Five;
					continue;
				}
				List<int> cardsWithJ = hand.Cards.Where(x => x != 0).ToList();
				IGrouping<int, int> largestGrouping = cardsWithJ.GroupBy(c => c).MaxBy(g => g.Count())!;
				int groupCount = cardsWithJ.GroupBy(ch => ch).Count();
				switch (groupCount)
				{
					// Five of a kind
					case 1:
						hand.Value = Value.Five;
						break;
					// Four of a kind and Full house
					case 2:
						hand.Value = largestGrouping.Count() + jCount == 4 ? Value.Four : Value.FullHouse;
						break;
					case 3:
					{
						// Three of a kind and Two pair
						hand.Value = largestGrouping.Count() + jCount == 3 ? Value.Three : Value.TwoPair;
						break;
					}
					// One pair
					case 4:
						hand.Value = Value.Pair;
						break;

					// High card
					default:
						hand.Value = Value.High;
						break;
				}
			}
			hands = hands.OrderByDescending(h => h.Value)
				.ThenBy(x => x.Cards[0])
				.ThenBy(x => x.Cards[1])
				.ThenBy(x => x.Cards[2])
				.ThenBy(x => x.Cards[3])
				.ThenBy(x => x.Cards[4])
				.ToList();

			ulong total = 0;
			ulong i = 1;
			foreach (Hand hand in hands)
			{
				total += (ulong)hand.Bet * i;
				Console.WriteLine($"{hand.Original} {(int)hand.Value}");
				i++;
			}

			Console.WriteLine(total);
		}
	}
}

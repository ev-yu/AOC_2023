using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day2
	{

		class GameSet
		{
			public int Index;
			public int Red;
			public int Green;
			public int Blue;

			public GameSet AddNumber(int number, string color)
			{
				switch (color)
				{
					case "red": Red += number; break;
					case "green": Green += number; break;
					case "blue": Blue += number; break;
				}

				return this;
			}
		}

		public static void Part1()
		{
			List<string> games = File.ReadAllLines("input.txt").Select(x => x.Split(": ").Last()).ToList();
			List<GameSet> validGames = new();
			// We set the loop counter to 1 here because the game ids start at 1
			int i = 1;
			// Loop over all games in the input
			foreach (string game in games)
			{
				// Initialize the set of counters for this game
				GameSet gameCounter = new() { Index = i };
				// Keep track of whether or not any set in this game has been invalid
				bool isGameValid = true;

				// For each game, get the "sets" played in that game
				List<string> sets = game.Split("; ").ToList();
				// Next, we will loop over every set in this game
				foreach (string set in sets)
				{
					// First, create a new counter for just this set
					GameSet setCounter = new();
					// Then, for every time a set of cubes is revealed
					foreach (string reveal in set.Split(", "))
					{
						// Find the number and color of cubes
						List<string> substr = reveal.Split(' ').ToList();
						int number = int.Parse(substr.First());
						string color = substr.Last();
						// And add those to the counter
						setCounter.AddNumber(number, color);
					}
					// Next, check if the set was valid for the wanted configuration
					isGameValid = setCounter.Red <= 12 && setCounter.Green <= 13 && setCounter.Blue <= 14;
					// If it isnt, we can bail early
					if (!isGameValid)
						break;

					// Otherwise, copy the set counters to the game counters, this might come in useful in part 2
					gameCounter.Red += setCounter.Red;
					gameCounter.Green += setCounter.Green;
					gameCounter.Blue += setCounter.Blue;
				}

				// If the game is valid, add it to the list of valid games
				if (isGameValid)
					validGames.Add(gameCounter);
				i++;
			}

			Console.WriteLine(validGames.Sum(x => x.Index));

		}

		public static void Part2()
		{
			List<string> games = File.ReadAllLines("input.txt").Select(x => x.Split(": ").Last()).ToList();
			List<GameSet> gameCounters = new();
			// We set the loop counter to 1 here because the game ids start at 1
			int i = 1;
			// Loop over all games in the input
			foreach (string game in games)
			{
				// Initialize the set of counters for this game
				GameSet gameCounter = new() { Index = i };

				// For each game, get the "sets" played in that game
				List<string> sets = game.Split("; ").ToList();
				// Next, we will loop over every set in this game
				foreach (string set in sets)
				{
					// First, create a new counter for just this set
					GameSet setCounter = new();
					// Then, for every time a set of cubes is revealed
					foreach (string reveal in set.Split(", "))
					{
						// Find the number and color of cubes
						List<string> substr = reveal.Split(' ').ToList();
						int number = int.Parse(substr.First());
						string color = substr.Last();
						// And add those to the counter
						setCounter.AddNumber(number, color);
					}

					// For each color, check if the amount used in this set is higher than the recorded highest amount for this game
					// If it is, set it to the higher value
					gameCounter.Red = int.Max(gameCounter.Red, setCounter.Red);
					gameCounter.Green = int.Max(gameCounter.Green, setCounter.Green);
					gameCounter.Blue = int.Max(gameCounter.Blue, setCounter.Blue);
				}
				// Add the game to the list of games
				gameCounters.Add(gameCounter);
				i++;
			}

			Console.WriteLine(gameCounters.Sum(x => x.Red * x.Green * x.Blue));
		}

		public static void Part2_Linq()
		{
			// Just for fun, let's do the entire puzzle in a single LINQ expression chain
			int value = File.ReadAllLines("input.txt").Select(x => x.Split(": ").Last() /* For each game */)
				.Select(game => game.Split("; ") // For each set in that game
					.Select(set =>
						set.Split(", ")
							.Select(reveal => 
								new GameSet().AddNumber(
									int.Parse(reveal.Split(' ').First()), 
									reveal.Split(' ').Last()
								)
							) // Keep track of the revealed number and color
							.Aggregate((a, b) => new GameSet()
							{
								Red = a.Red + b.Red,
								Green = a.Green + b.Green,
								Blue = a.Blue + b.Blue
							})
					)
					.Aggregate((a, b) => new GameSet
					{
						Red = int.Max(a.Red, b.Red),
						Green = int.Max(a.Green, b.Green),
						Blue = int.Max(a.Blue, b.Blue),
					})
				)
				.Sum(x => x.Red * x.Green * x.Blue);
			Console.WriteLine(value);
		}
	}
}

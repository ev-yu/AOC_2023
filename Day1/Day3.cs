using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
	internal class Day3
	{
		public static void Part1()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int lineNo = 0;
			List<int> foundNums = new();
			foreach (string line in lines)
			{
				int i = 0;
				string currentNum = "";
				// Off-by-one-error if we dont do one more iteration than there are chars
				while (i <= line.Length)
				{
					// If the current character is a digit, add it to the currentNum
					if (i < line.Length && char.IsDigit(line[i]))
					{
						currentNum += line[i];
					}
					// If it isn't, we check if we found a number, and then check for adjacent symbols
					else
					{
						// If we actually found a number
						if (!string.IsNullOrEmpty(currentNum))
						{
							int startIndex = int.Max(0, i - currentNum.Length - 1);
							int length = int.Min(line.Length - startIndex, currentNum.Length + 2);
							string searchUp = lines[int.Max(0, lineNo - 1)].Substring(startIndex, length);
							string searchEqual = lines[lineNo].Substring(startIndex, length);
							string searchDown = lines[int.Min(lines.Count - 1, lineNo + 1)].Substring(startIndex, length);

							bool touchesSymbol = searchUp.Any(x => !char.IsDigit(x) && x != '.') || searchEqual.Any(x => !char.IsDigit(x) && x != '.') || searchDown.Any(x => !char.IsDigit(x) && x != '.');
							if (touchesSymbol)
							{
								foundNums.Add(int.Parse(currentNum));
							}
						}
						currentNum = "";
					}
					i++;
				}
				lineNo++;
			}

			int end = foundNums.Sum();
			Console.WriteLine(end);
		}

		class FoundNumber
		{
			public int Line;
			public int Value;
			public int StartIndex;
			public int Length;
			public int GearIndex;
		}

		public static void Part2()
		{
			List<string> lines = File.ReadAllLines("input.txt").ToList();
			int lineNo = 0;
			int lineLength = lines.First().Length;
			List<FoundNumber> foundNums = new();
			foreach (string line in lines)
			{
				int i = 0;
				string currentNum = "";
				// Off-by-one-error if we dont do one more iteration than there are chars
				while (i <= line.Length)
				{
					// If the current character is a digit, add it to the currentNum
					if (i < line.Length && char.IsDigit(line[i]))
					{
						currentNum += line[i];
					}
					// If it isn't, we check if we found a number, and then check for adjacent symbols
					else
					{
						// If we actually found a number
						if (!string.IsNullOrEmpty(currentNum))
						{
							int startIndex = int.Max(0, i - currentNum.Length - 1);
							int length = int.Min(line.Length - startIndex, currentNum.Length + 2);
							string searchUp = lines[int.Max(0, lineNo - 1)].Substring(startIndex, length);
							string searchEqual = lines[lineNo].Substring(startIndex, length);
							string searchDown = lines[int.Min(lines.Count - 1, lineNo + 1)].Substring(startIndex, length);

							bool touchesSymbol = searchUp.Any(x => !char.IsDigit(x) && x != '.') || searchEqual.Any(x => !char.IsDigit(x) && x != '.') || searchDown.Any(x => !char.IsDigit(x) && x != '.');
							if (touchesSymbol)
							{
								int gearLine = -1;
								int gearIndex = 0;
								int indexGearUp = searchUp.IndexOf('*');
								int indexGearEqual = searchEqual.IndexOf('*');
								int indexGearDown = searchDown.IndexOf('*');
								if (indexGearEqual >= 0)
								{
									gearIndex = indexGearEqual + startIndex;
									gearLine = lineNo;
								}
								else if (indexGearUp >= 0)
								{
									gearIndex = indexGearUp + startIndex;
									gearLine = lineNo - 1;
								}
								else if (indexGearDown >= 0)
								{
									gearIndex = indexGearDown + startIndex;
									gearLine = lineNo + 1;
								}

								if (gearLine != -1)
								{
									foundNums.Add(new FoundNumber()
									{
										Line = lineNo,
										Value = int.Parse(currentNum),
										StartIndex = startIndex,
										Length = length,
										GearIndex = gearLine * lineLength + gearIndex
									});
								}
								
							}
						}
						currentNum = "";
					}
					i++;
				}
				lineNo++;
			}

			// Find all of the pairs of numbers that touch the same gear
			var gearPairs = foundNums.GroupBy(x => x.GearIndex).Where(x => x.Count() == 2).ToList();
			int total = gearPairs.Select(x => x.First().Value * x.Last().Value).Sum();
			Console.WriteLine(total);
		}
	}
}

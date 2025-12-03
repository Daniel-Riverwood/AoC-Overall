using System.Globalization;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 03, CodeType.Original)]
public class Day_03_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = ProcessInput1(input.Lines);

		var part2 = ProcessInput2(input.Lines);

		return (part1, part2);
	}

	private string ProcessInput1(string[] input)
	{
		long sum = 0;

		foreach (var line in input)
		{
			List<int> intlist = line.ToCharArray().Select(q => (int)q - '0').ToList();
			long active = 0;
			int surplus = 1;
			string curmax = string.Empty;
			List<int> curBank = new List<int>(intlist);

			while (active < 2)
			{
				if (surplus == 0)
				{
					curmax += curBank.Max().ToString();
					break;
				}

				int validMax = curBank.Take(curBank.Count - surplus).Max();
				int validMaxPos = curBank.IndexOf(validMax) + 1;

				curmax += validMax.ToString();
				surplus -= 1;
				active += 1;
				curBank = curBank.Skip(validMaxPos).ToList();
			}

			sum += long.Parse(curmax);
		}

		return $"{sum}";
	}

	private string ProcessInput2(string[] input)
	{
		long sum = 0;

		foreach (var line in input)
		{
			List<int> intlist = line.ToCharArray().Select(q => (int)q - '0').ToList();
			long active = 0;
			int surplus = 11;
			string curmax = string.Empty;
			List<int> curBank = new List<int>(intlist);

			while(active < 12)
			{
				if (surplus == 0)
				{
					curmax += curBank.Max().ToString();
					break;
				}

				int validMax = curBank.Take(curBank.Count - surplus).Max();
				int validMaxPos = curBank.IndexOf(validMax) + 1;

				curmax += validMax.ToString();
				surplus -= 1;
				active += 1;
				curBank = curBank.Skip(validMaxPos).ToList();
			}

			sum += long.Parse(curmax);

		}

		return $"{sum}";
	}
}

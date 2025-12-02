using System.Globalization;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 02, CodeType.Fastest)]
public class Day_02_Fastest : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = ProcessInput1(input.Text);

		var part2 = ProcessInput2(input.Text);

		return (part1, part2);
	}

	private string ProcessInput1(string input)
	{
		long sum = 0;
		var parts = input.Split(",");
		var p = new Regex("^(.+)\\1$");

		foreach (var part in parts)
		{
			var split = part.Split("-");
			_ = long.TryParse(split[0], out var start);
			_ = long.TryParse(split[1], out var end);
			for (var i = start; i <= end; i++)
			{
				var val = i.ToString();
				if (p.Match(val).Success)
				{
					sum += i;
				}
			}
		}

		return $"{sum}";
	}

	private string ProcessInput2(string input)
	{
		long sum = 0;
		var parts = input.Split(",");
		var p = new Regex("^(.+)\\1+$");

		foreach (var part in parts)
		{
			var split = part.Split("-");
			_ = long.TryParse(split[0], out var start);
			_ = long.TryParse(split[1], out var end);

			for (var i = start; i <= end; i++)
			{
				var val = i.ToString();
				if (p.Match(val).Success)
				{
					sum += i;
				}
			}
		}

		return $"{sum}";
	}
}

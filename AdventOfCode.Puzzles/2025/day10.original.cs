using System.Globalization;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 10, CodeType.Original)]
public class Day_10_Original : IPuzzle
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
		foreach (var lines in input)
		{
			long minpresses = long.MaxValue;
			var priorityQueue = new PriorityQueue<(int[] buttons, bool[] lights), int>();
			var splits = lines.Split(" ");
			var finalTarget = splits.First().Trim('[').Trim(']');
			var buttonIndexes = splits.Skip(1).SkipLast(1).ToList();
			bool[] target = new bool[finalTarget.Length];
			int[][] buttons = new int[buttonIndexes.Count][];
			for (int x = 0; x < finalTarget.Length; x++)
			{
				if (finalTarget[x] == '#')
				{
					target[x] = true;
				}
			}

			for (int x = 0; x < buttonIndexes.Count; x++)
			{
				var buttonoptions = buttonIndexes[x].Split(',');
				buttons[x] = new int[buttonoptions.Length];
				for (int y = 0; y < buttonoptions.Length; y++)
				{
					buttonoptions[y] = buttonoptions[y].Trim('(').Trim(')');
					if (int.TryParse(buttonoptions[y], out int validButton))
					{
						buttons[x][y] = validButton;
					}
				}
				priorityQueue.Enqueue((buttons[x], new bool[finalTarget.Length]), 0);
			}


			while (priorityQueue.TryDequeue(out var element, out int presses))
			{
				if (Enumerable.SequenceEqual(element.lights, target))
				{
					if (minpresses >= presses)
					{
						minpresses = presses;
						break;
					}
				}

				var newLights = new bool[element.lights.Length];
				for (int x = 0; x < element.lights.Length; x++)
				{
					newLights[x] = element.lights[x];
				}
				for (int x = 0; x < element.buttons.Length; x++)
				{
					newLights[element.buttons[x]] = !newLights[element.buttons[x]];
				}

				int pressCount = presses + 1;
				for (int x = 0; x < buttons.Length; x++)
				{
					priorityQueue.Enqueue((buttons[x], newLights), pressCount);
				}
			}

			sum += minpresses;
		}
		return $"{sum}";
	}
	
	private string ProcessInput2(string[] input)
	{
		long sum = 0;
		input.AsParallel().ForEach(lines =>
		{
			long minpresses = long.MaxValue;
			var priorityQueue = new PriorityQueue<(int[] buttons, int[] joltage), int>();
			var splits = lines.Split(" ");
			var finalTarget = splits.Last().Trim('{').Trim('}').Split(',');
			var buttonIndexes = splits.Skip(1).SkipLast(1).ToList();
			int[] target = new int[finalTarget.Length];
			int[][] buttons = new int[buttonIndexes.Count][];
			for (int x = 0; x < finalTarget.Length; x++)
			{
				target[x] = int.Parse(finalTarget[x]);
			}

			for (int x = 0; x < buttonIndexes.Count; x++)
			{
				var buttonoptions = buttonIndexes[x].Split(',');
				buttons[x] = new int[buttonoptions.Length];
				for (int y = 0; y < buttonoptions.Length; y++)
				{
					buttonoptions[y] = buttonoptions[y].Trim('(').Trim(')');
					if (int.TryParse(buttonoptions[y], out int validButton))
					{
						buttons[x][y] = validButton;
					}
				}
				priorityQueue.Enqueue((buttons[x], new int[target.Length]), 0);
			}


			while (priorityQueue.TryDequeue(out var element, out int presses))
			{
				if (Enumerable.SequenceEqual(element.joltage, target))
				{
					if (minpresses >= presses)
					{
						minpresses = presses;
						break;
					}
				}

				var newJoltage = new int[element.joltage.Length];
				for (int x = 0; x < element.joltage.Length; x++)
				{
					newJoltage[x] = element.joltage[x];
				}
				var canBreak = false;
				for (int x = 0; x < element.buttons.Length; x++)
				{
					newJoltage[element.buttons[x]]++;
					if (newJoltage[element.buttons[x]] > target[element.buttons[x]])
					{
						canBreak = true;
						break;
					}
				}

				if (canBreak)
				{
					continue;
				}
				int pressCount = presses + 1;
				for (int x = 0; x < buttons.Length; x++)
				{
					priorityQueue.Enqueue((buttons[x], newJoltage), pressCount);
				}
			}

			sum += minpresses;
		});
		return $"{sum}";
	}
}

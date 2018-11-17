using System;

namespace Kontur.Courses.Git
{
	class Program
	{
		private static void Main()
		{
			Calculator calculator = new Calculator();
			RunLoop(calculator);
		}

		private static void RunLoop(Calculator calculator)
		{
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				var line = Console.ReadLine();
				if (line == null) break;
				var args = Calculator.SplitInput(line);
				var result = calculator.Calculate(args);
				Console.ForegroundColor = result.HasValue ? ConsoleColor.Green : ConsoleColor.Red;
				Console.WriteLine("> " + result);
			}
		}
	}
}

using System.Collections.Generic;
using System.Linq;

namespace Kontur.Courses.Git
{
	public class Calculator
	{
		private Maybe<double> lastResult = 0;

		public Maybe<double> Calculate(string[] args)
		{
			if (args.Length == 0)
				return lastResult;
			if (args.Length == 1)
				return lastResult = double.Parse(args[0]);
			if (args.Length == 2)
			{
				var v2 = double.Parse(args[1]);
				return lastResult = Execute(args[0], lastResult.Value, v2);
			}
			if (args.Length == 3)
			{
				var v1 = double.Parse(args[0]);
				var v2 = double.Parse(args[2]);
				return lastResult = Execute(args[1], v1, v2);
			}
			return Maybe<double>.FromError("Error input");
		}

		private Maybe<double> Execute(string op, double v1, double v2)
		{
			if (op == "+")
				return v1 + v2;
			if (op == "-")
				return v1 - v2;
			if (op == "*")
				return v1 - v2;
			if (op == "/")
				return v1 / v2;
			return Maybe<double>.FromError("Unknown operation '{0}'", op);
		}

		public static string[] SplitInput(string line)
		{
			if (line.Length == 0) return new string[0];
			List<string> res = new List<string> { "" };
			bool isDigit = char.IsDigit(line[0]);
			foreach (var ch in line)
			{
				if (char.IsDigit(ch) != isDigit)
				{
					res.Add("");
					isDigit = !isDigit;
				}
				if (!char.IsWhiteSpace(ch))
					res[res.Count - 1] += ch;
			}
			return res.ToArray();
		}
	}
}
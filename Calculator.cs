using System.Collections.Generic;
using System.Linq;

namespace Kontur.Courses.Git
{
	public class Calculator
	{
		private Maybe<double> lastResult = 0;
		private Maybe<double> TryParseDouble(string s)
		{
			double v;
			if (double.TryParse(s, out v))
				return v;
			return Maybe<double>.FromError("Not a number '{0}'", s);
		}

		public Maybe<double> Calculate(string[] args)
		{
			if (args.Length == 0)
				return lastResult;
			if (args.Length == 1)
			{
				var result = TryParseDouble(args[0]);
				if (result.HasValue)
					lastResult = result;
				return result;
			}
			if (args.Length == 3)
			{
				var v1 = TryParseDouble(args[0]);
				var v2 = TryParseDouble(args[2]);
				if (!v1.HasValue) return v1;
				if (!v2.HasValue) return v2;
				var result = Execute(args[1], v1.Value, v2.Value);
				if (result.HasValue)
					lastResult = result;
				return result;
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
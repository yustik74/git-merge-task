using System;
using NUnit.Framework;

namespace Kontur.Courses.Git
{
	[TestFixture]
	public class Calculator_Should
	{
		private Calculator calc;

		[SetUp]
		public void SetUp()
		{
			calc = new Calculator();
		}

		public Maybe<double> Calc(string input)
		{
			var args = input.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
			return calc.Calculate(args);
		}

		[Test]
		public void OneArg()
		{
			Assert.AreEqual(42, Calc("42").Value);
			Assert.AreEqual(43, Calc("43").Value);
		}

		[Test]
		public void ZeroArg()
		{
			Assert.AreEqual(42, Calc("42").Value);
			Assert.AreEqual(42, Calc("").Value);
			Assert.AreEqual(42, Calc("").Value);
		}

		[Test]
		public void ThreeArg()
		{
			Assert.AreEqual(55, Calc("42 + 13").Value);
			Assert.AreEqual(1, Calc("2 - 1").Value);
		}

		[TestCase("1", ExpectedResult = new[] { "1" })]
		[TestCase("2+3", ExpectedResult = new[] { "2", "+", "3" })]
		[TestCase("45-67", ExpectedResult = new[] { "45", "-", "67" })]
		[TestCase("8 - 9", ExpectedResult = new[] { "8", "-", "9" })]
		[TestCase("01 -    234", ExpectedResult = new[] { "01", "-", "234" })]
		public string[] SplitInput(string input)
		{
			return Calculator.SplitInput(input);
		}

		[TestCase("asd", "+", "2")]
		[TestCase("2", "+", "asd")]
		[TestCase("asd", "+", "asd")]
		[TestCase("asd", "asd", "asd")]
		[TestCase("2", "asd", "3")]
		public void ThreeArg_BadInput(params string[] args)
		{
			var calc = new Calculator();
			calc.Calculate(new[] { "5" });
			Assert.IsFalse(calc.Calculate(args).HasValue);
			Assert.AreEqual(5.0, calc.Calculate(new string[] { }).Value);
		}

		[Test]
		public void OneArg_BadInput()
		{
			var calc = new Calculator();
			calc.Calculate(new[] { "5" });
			Assert.IsFalse(calc.Calculate(new[] { "asd" }).HasValue);
			Assert.AreEqual(5.0, calc.Calculate(new string[] { }).Value);
		}

	}
}
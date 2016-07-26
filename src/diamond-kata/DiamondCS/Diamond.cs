
using System;
using System.Collections.Generic;
using System.Linq;

// Print a diamond based on a letter, there the letter determines both the
// content and size of the diamond.
//
//
// A returns:
// 
// A
//
// B returns:
//
//  A
// B B
//  A
//
// C returns:
//
//   A
//  B B 
// C   C
//  B B 
//   A

namespace Diamond.CSharp
{
	public static class Diamonds
	{
		static int Size(char ch) => ch - 'A' + 1;

		static int CharNum(char ch) => ch - 'A';

		static char NumChar(int x) => (char)('A' + x);

		static string Join(this IEnumerable<string> source, string separator = "") =>
			String.Join(separator, source.ToArray());

		static string Pad(int count, char c = ' ') =>
			Enumerable.Repeat(c.ToString(), count)
					  .Join();

		public static string Diamond1(char ch) => "";

		public static string Diamond2(char ch) => ch.ToString();

		public static string Diamond3(char ch) =>
			Enumerable.Range(0, Size(ch) * 2 - 2)
					  .Select(x => x.ToString())
					  .Join("\n");

		public static string Diamond4(char ch) =>
			Enumerable.Range(0, Size(ch) * 2 - 2)
					  .Select(NumChar)
					  .Select(x => x.ToString())
					  .Join("\n");

		public static string Diamond5(char ch)
		{
			var n = CharNum(ch);
			return Enumerable.Range(0, n+1)
				             .Concat(Enumerable.Range(0, n).Reverse())
				             .Select(x => $"{Pad(n-x, ' ')}{NumChar(x)}")
							 .Join("\n");
		}


		public static string Diamond6(char ch)
		{
			var n = CharNum(ch);
			return Enumerable.Range(0, n+1)
							 .Concat(Enumerable.Range(0, n).Reverse())
				             .Select(x => $"{Pad(n - x, ' ')}{Pad((x + 1) * 2 - 1, NumChar(x))}")
							 .Join("\n");
		}

		public static string Diamond7(char ch)
		{
			var n = CharNum(ch);
			return Enumerable.Range(0, n+1)
							 .Concat(Enumerable.Range(0, n).Reverse())
							 .Select(x =>
									 x == 0 ? $"{Pad(n - x, ' ')}{Pad((x + 1) * 2 - 1, NumChar(x))}"
									 : $"{Pad(n - x, ' ')}{NumChar(x)}{Pad(x * 2 - 1, ' ')}{NumChar(x)}")
							 .Join("\n");
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colibri;

namespace Solutions
{
	internal class Sandbox
	{
		public static void test()
		{
			List<string> test = new() {"test1", "test2", "test3"};
			var test1 = test.Select(x => new { Member1 = x, Member2 = x.StringReverse() });
			foreach (var obj in test1)
			{
				Console.WriteLine($"{obj.Member1}, {obj.Member2}");
			}
		}
	}
}

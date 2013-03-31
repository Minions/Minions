using System;
using Fools.cs.AST;

namespace Fools.cs
{
	public class FoolsParser
	{
		public static ProgramFragment find_blocks(string source_code)
		{
			var result = new ProgramFragment();
			var lines = source_code.Split(new[] {"\n", "\r\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
			foreach(var line in lines)
			{
				var l = line.Trim();
				if (l.EndsWith(":"))
				{
					
				}
			}
			return result;
		}
	}
}

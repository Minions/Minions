using System;
using Fools.cs.AST;

namespace Fools.cs
{
	public class FoolsParser
	{
		public static ProgramFragment find_blocks(string source_code)
		{
			var result = new ProgramFragment();
			result.declarations.Add(new FoolsPegParser().Parse(source_code, "fake file name.fool"));
			return result;
		}
	}
}

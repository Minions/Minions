using System;
using System.Collections.Generic;
using Fools.cs.AST;
using System.Linq;

namespace Fools.cs
{
	public class FoolsParser
	{
		public static ProgramFragment find_blocks(string source_code)
		{
			var result = new ProgramFragment();
			var raw_parse = (IEnumerable<object>) new FoolsPegParser().Parse(source_code, "fake file name.fool");
			result.declarations.AddRange(raw_parse.without_nulls());
			return result;
		}
	}
}

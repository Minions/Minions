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
			try
			{
				var raw_parse = (IEnumerable<object>)new FoolsPegParser().Parse(source_code, "fake file name.fool");
				result.declarations.AddRange(raw_parse.without_nulls());
			}
			catch (FormatException e)
			{
				result.errors.Add(e.Message);
			}
			catch (FatalParseError e)
			{
				result.errors.Add(e.error);
			}
			return result;
		}
	}
}

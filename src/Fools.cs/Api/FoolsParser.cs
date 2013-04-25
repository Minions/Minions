using System;
using Fools.cs.AST;
using Fools.cs.ParseToAst;
using Pegasus.Common;

namespace Fools.cs.Api
{
	public class FoolsParser
	{
		public static ProgramFragment find_blocks(string source_code, string file_name)
		{
			var result = new ProgramFragment();
			var parser = new FoolsPegParser {report = new Report(result)};
			try
			{
				var raw_parse = parser.Parse(source_code, file_name);
				result.declarations.AddRange(raw_parse.without_nulls());
			}
			catch(FormatException e)
			{
				result.errors.Add(new ErrorReport(e.Message, (Cursor) e.Data["cursor"], null, null, null, "Hopefully I also gave you some more specific error messages. Try fixing those first."));
			}
			catch(FatalParseError e)
			{
				result.errors.Add(e.error);
			}
			return result;
		}
	}
}

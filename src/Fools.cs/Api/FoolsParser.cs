// FoolsParser.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Diagnostics;
using Fools.cs.AST;
using Fools.cs.ParseToAst;
using Fools.cs.Utilities;
using Pegasus.Common;

namespace Fools.cs.Api
{
	public static class FoolsParser
	{
		[NotNull]
		public static ProgramFragment find_blocks(string source_code, string file_name)
		{
			var result = new ProgramFragment();
			var parser = new FoolsPegParser {report = new Report(result)};
			try
			{
				var raw_parse = parser.Parse(source_code, file_name);
				Debug.Assert(raw_parse != null, "raw_parse != null");
				result.declarations.AddRange(raw_parse.without_nulls());
			}
			catch (FormatException e)
			{
				result.errors.Add(new ErrorReport(e.Message,
					(Cursor) e.Data["cursor"],
					null,
					null,
					null,
					"Hopefully I also gave you some more specific error messages. Try fixing those first."));
			}
			catch (FatalParseError e)
			{
				result.errors.Add(e.error);
			}
			return result;
		}
	}
}

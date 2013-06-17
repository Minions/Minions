// Interpreter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Linq;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.Interpret
{
	public class Interpreter : IDisposable
	{
		public void Dispose()
		{
		}

		public void take_commands([NotNull] string fools_command_file_contents)
		{
			var parse = FoolsParser.find_blocks(fools_command_file_contents, "interpreter");
			if (parse.errors.Count > 0) throw new AggregateException(parse.errors.Cast<Exception>());
		}
	}
}

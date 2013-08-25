// CompileProjects.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Utilities;
using PowerArgs;

namespace core_compile
{
	internal class CompileProjects
	{
		[NotNull] private readonly Program _program;

		public CompileProjects([NotNull] Program program)
		{
			_program = program;
		}

		public static void print_usage([NotNull] CompileProjects lab, [NotNull] AppAbort message)
		{
			lab.print_usage(message.exception, message.error_level);
		}

		public static void run([NotNull] CompileProjects lab, AppRun message)
		{
			Console.WriteLine("I would be parsing the project file here.");
			lab._program.exit(Program.ErrorLevel.Ok);
		}

		private void print_usage([CanBeNull] Exception exception, Program.ErrorLevel error_level)
		{
			if (exception != null) Console.WriteLine(exception.Message);
			ArgUsage.GetStyledUsage<CompilerUserInteractionModel>()
				.Write();
			_program.exit(error_level);
		}
	}
}

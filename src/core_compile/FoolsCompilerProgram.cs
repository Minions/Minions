// FoolsCompilerProgram.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Utilities;
using Fools.cs.builtins;

namespace core_compile
{
	public class FoolsCompilerProgram : MissionSpecification
	{
		[NotNull] private readonly Program _program;

		private FoolsCompilerProgram([NotNull] Program program) : base("Parse the project file and find work to do")
		{
			_program = program;
		}

		[NotNull]
		public static MissionSpecification make_primary_mission([NotNull] Program program)
		{
			return new FoolsCompilerProgram(program);
		}

		public override void execute(MissionOperator operation)
		{
			Console.WriteLine("I would be parsing the project file here.");
			_program.exit(Program.ErrorLevel.Ok);
		}
	}
}

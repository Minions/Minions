// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;
using Fools.cs.Utilities;

namespace core_compile
{
	[UsedImplicitly]
	public class Program
	{
		// ReSharper disable InconsistentNaming
		private static int Main([NotNull] string[] args) // ReSharper restore InconsistentNaming
		{
			using (var mission_control = new MissionControl())
			{
				CommandLineProgram<CompilerUserInteractionModel>.prepare_missions(mission_control);
				CompileProjects.prepare_missions(mission_control);

				mission_control.announce(new AppInit(args));
				return (int) mission_control.overlord_throne.watch_the_fools_dance();
			}
		}
	}
}

// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;
using Fools.cs.Api.CommandLineApp;
using Fools.cs.Utilities;

namespace core_compile
{
	[PublicAPI]
	public class Program
	{
		// ReSharper disable InconsistentNaming
		private static int Main([NotNull] string[] args) // ReSharper restore InconsistentNaming
		{
			using (var mission_control = new MissionControl())
			{
				CommandLineProgram<CompilerUserInteractionModel>.submit_missions_to(mission_control);
				CompileProjects.submit_missions_to(mission_control);

				mission_control.announce(new DoMyBidding(args));
				return (int) mission_control.overlord_throne.watch_the_fools_dance();
			}
		}
	}
}

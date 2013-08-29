// CompileProjects.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Api.CommandLineApp;
using Fools.cs.Utilities;

namespace core_compile
{
	internal class CompileProjects
	{
		[NotNull] private readonly MissionControl _mission_control;

		private CompileProjects([NotNull] MissionControl mission_control)
		{
			_mission_control = mission_control;
		}

		public static void submit_missions_to([NotNull] MissionControl mission_control)
		{
			var watch_for_projects_to_compile = new MissionDescription<CompileProjects>(() => new CompileProjects(mission_control));
			watch_for_projects_to_compile.send_new_fool_when<AppRun<CompilerUserInteractionModel>>()
				.and_have_it(run);
			mission_control.send_out_fools_to(watch_for_projects_to_compile);
		}

		private static void run([NotNull] CompileProjects lab, AppRun<CompilerUserInteractionModel> message)
		{
			Console.WriteLine("I would be parsing the project file here.");
			lab._mission_control.announce(new AppQuit(AppErrorLevel.Ok));
		}
	}
}

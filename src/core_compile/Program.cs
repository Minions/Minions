// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;
using Fools.cs.Api;
using Fools.cs.Utilities;
using PowerArgs;

namespace core_compile
{
	[UsedImplicitly]
	public class Program
	{
		[NotNull] private readonly MissionControl _mission_control = new MissionControl();
		private ErrorLevel _result = ErrorLevel.Ok;
		[NotNull] private readonly ManualResetEventSlim _program_complete = new ManualResetEventSlim(false);

		public enum ErrorLevel
		{
			Ok = 0,
			BadCommandArgs = 1,
			Unknown = 2,
		}

		// ReSharper disable InconsistentNaming
		private static int Main([NotNull] string[] args) // ReSharper restore InconsistentNaming
		{
			var commands = figure_out_what_user_wants_to_do<CompilerUserInteractionModel>(args);
			return (int) prepare_missions(commands)
				.execute();
		}

		[NotNull]
		private static Program prepare_missions([NotNull] Commands<CompilerUserInteractionModel> commands)
		{
			var program = new Program();

			var mission = new MissionDescription<CompileProjects>(() => new CompileProjects(program));
			mission.spawns_when<AppAbort>()
				.and_does(CompileProjects.print_usage);
			mission.spawns_when<AppRun>()
				.and_does(CompileProjects.run);

			program._mission_control.execute_as_needed(mission);

			if (commands.args == null || commands.args.help) program._mission_control.announce(new AppAbort(commands.exception, commands.error_level));
			else program._mission_control.announce(new AppRun(commands));

			return program;
		}

		public void exit(ErrorLevel result)
		{
			_result = result;
			_program_complete.Set();
		}

		private ErrorLevel execute()
		{
			_program_complete.Wait();
			_mission_control.Dispose();
			return _result;
		}

		[NotNull]
		private static Commands<T> figure_out_what_user_wants_to_do<T>([NotNull] string[] args) where T : UniversalCommands
		{
			try
			{
				return Commands<T>.run(Args.Parse<T>(args));
			}
			catch (ArgException ex)
			{
				return Commands<T>.quit(ErrorLevel.BadCommandArgs, ex);
			}
			catch (Exception ex)
			{
				return Commands<T>.quit(ErrorLevel.Unknown, ex);
			}
		}
	}
}

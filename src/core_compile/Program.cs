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
			var program = new Program();
			prepare_missions(program);
			program.begin_execution(args);
			return (int) program.execute_until_program_terminates();
		}

		private void begin_execution([NotNull] string[] args)
		{
			CompilerUserInteractionModel user_commands;
			try
			{
				user_commands = Args.Parse<CompilerUserInteractionModel>(args);
			}
			catch (ArgException ex)
			{
				_mission_control.announce(new AppAbort(ex, ErrorLevel.BadCommandArgs));
				return;
			}
			catch (Exception ex)
			{
				_mission_control.announce(new AppAbort(ex, ErrorLevel.Unknown));
				return;
			}
			if (user_commands.help) _mission_control.announce(new AppAbort(null, ErrorLevel.Ok));
			else _mission_control.announce(new AppRun(user_commands));
		}

		private static void prepare_missions([NotNull] Program program)
		{
			var mission = new MissionDescription<CompileProjects>(() => new CompileProjects(program));
			mission.spawns_when<AppAbort>()
				.and_does(CompileProjects.print_usage);
			mission.spawns_when<AppRun>()
				.and_does(CompileProjects.run);

			program.add_mission(mission);
		}

		public void add_mission<T>([NotNull] MissionDescription<T> mission) where T : class
		{
			_mission_control.execute_as_needed(mission);
		}

		public void exit(ErrorLevel result)
		{
			_result = result;
			_program_complete.Set();
		}

		private ErrorLevel execute_until_program_terminates()
		{
			_program_complete.Wait();
			_mission_control.Dispose();
			return _result;
		}
	}
}

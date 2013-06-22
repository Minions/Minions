// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;
using Fools.cs.Api;
using Fools.cs.Utilities;
using Fools.cs.builtins;
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

		// ReSharper disable UnusedParameter.Local
		[NotNull]
		private static Program prepare_missions([NotNull] Commands<CompilerUserInteractionModel> commands)
			// ReSharper restore UnusedParameter.Local
		{
			var program = new Program();
			SinglePartMission mission;
			if (commands.args == null || commands.args.help) mission = new SinglePartMission("Print usage", () => program.print_usage(commands.exception, commands.error_level));
			else mission = new SinglePartMission("Parse the project file and find work to do", program.parse_project_file);
			program._mission_control.accomplish(mission);
			return program;
		}

		private void parse_project_file()
		{
			Console.WriteLine("I would be parsing the project file here.");
			exit(ErrorLevel.Ok);
		}

		private void exit(ErrorLevel result)
		{
			_result = result;
			_program_complete.Set();
		}

		private void print_usage([CanBeNull] Exception exception, ErrorLevel error_level)
		{
			if (exception != null) Console.WriteLine(exception.Message);
			ArgUsage.GetStyledUsage<CompilerUserInteractionModel>()
				.Write();
			exit(error_level);
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

	public class UniversalCommands
	{
		[ArgDescription("Print this usage info and exit."), ArgShortcut("--help"), ArgShortcut("/?")]
		public bool help { get; set; }

		[ArgDescription("Run all built-in tests for this program and exit."), ArgShortcut("--run-tests")]
		public string test { get; set; }
	}

	public class Commands<T> where T : class
	{
		private Program.ErrorLevel _error_level = Program.ErrorLevel.Ok;
		[CanBeNull] private Exception _exception;

		private Commands(T args)
		{
			this.args = args;
		}

		[CanBeNull]
		public T args { get; private set; }

		public Program.ErrorLevel error_level { get { return _error_level; } }

		[CanBeNull]
		public Exception exception { get { return _exception; } }

		[NotNull]
		public static Commands<T> quit(Program.ErrorLevel error_level, [CanBeNull] Exception exception)
		{
			var commands = new Commands<T>(null) {_error_level = error_level, _exception = exception};
			return commands;
		}

		[NotNull]
		public static Commands<T> run([NotNull] T args)
		{
			return new Commands<T>(args);
		}
	}
}

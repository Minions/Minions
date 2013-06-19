// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Utilities;
using PowerArgs;

namespace core_compile
{
	[UsedImplicitly]
	public class Program
	{
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
			prepare_missions(commands);
			return (int) execute();
		}

// ReSharper disable UnusedParameter.Local
		private static void prepare_missions([NotNull] Commands<CompilerUserInteractionModel> commands)
// ReSharper restore UnusedParameter.Local
		{
			// if errors or -help, set up those missions. Else a compile mission.
			// Also set up ports, such as binding view model to view.
		}

		private static ErrorLevel execute()
		{
			return ErrorLevel.Ok;
		}

		[NotNull]
		private static Commands<T> figure_out_what_user_wants_to_do<T>([NotNull] string[] args) where T : UniversalCommands
		{
			try
			{
				return Args.Parse<Commands<T>>(args);
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
		public string help { get; set; }
	}

	public class Commands<T>
	{
		private Program.ErrorLevel _error_level;
		[CanBeNull] private Exception _exception;

		[CanBeNull]
		public T data { get; set; }

		public Program.ErrorLevel error_level { get { return _error_level; } }

		[CanBeNull]
		public Exception exception { get { return _exception; } }

		[NotNull]
		public static Commands<T> quit(Program.ErrorLevel error_level, [CanBeNull] Exception exception)
		{
			var commands = new Commands<T> {_error_level = error_level, _exception = exception};
			return commands;
		}
	}
}

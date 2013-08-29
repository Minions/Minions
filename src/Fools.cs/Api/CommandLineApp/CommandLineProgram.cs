// CommandLineProgram.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Diagnostics;
using Fools.cs.Utilities;
using PowerArgs;

namespace Fools.cs.Api.CommandLineApp
{
	public class CommandLineProgram<TViewModel> where TViewModel : UniversalCommands
	{
		[NotNull] private readonly MissionControl _mission_control;

		private CommandLineProgram([NotNull] MissionControl mission_control)
		{
			_mission_control = mission_control;
		}

		public static void submit_missions_to([NotNull] MissionControl mission_control)
		{
			var interact_with_user = new MissionDescription<CommandLineProgram<TViewModel>>(() => new CommandLineProgram<TViewModel>(mission_control));
			interact_with_user.spawns_when<DoMyBidding>()
				.and_does(figure_out_what_the_user_wants);
			interact_with_user.responds_to_message<AppAbort>(print_usage);
			mission_control.execute_as_needed(interact_with_user);
		}

		private static void figure_out_what_the_user_wants([NotNull] CommandLineProgram<TViewModel> lab,
			[NotNull] DoMyBidding message)
		{
			lab.figure_out_what_the_user_wants(message.args);
		}

		private static void print_usage([NotNull] CommandLineProgram<TViewModel> lab, [NotNull] AppAbort message)
		{
			lab.print_usage(message.exception, message.error_level);
		}

		private void figure_out_what_the_user_wants([NotNull] string[] args)
		{
			TViewModel user_commands;
			try
			{
				user_commands = Args.Parse<TViewModel>(args);
			}
			catch (Exception ex)
			{
				_mission_control.announce(new AppAbort(ex, ex is ArgException ? AppErrorLevel.BadCommandArgs : AppErrorLevel.Unknown));
				return;
			}
			Debug.Assert(user_commands != null, "user_commands != null");
			if (user_commands.help) _mission_control.announce(new AppAbort(null, AppErrorLevel.Ok));
			else _mission_control.announce(new AppRun<TViewModel>(user_commands));
		}

		private void print_usage([CanBeNull] Exception exception, AppErrorLevel error_level)
		{
			// ReSharper disable PossibleNullReferenceException
			ArgUsage.GetStyledUsage<TViewModel>()
				// ReSharper restore PossibleNullReferenceException
				.Write();
			if (exception != null) Console.WriteLine(exception.Message);
			_mission_control.announce(new AppQuit(error_level));
		}
	}
}

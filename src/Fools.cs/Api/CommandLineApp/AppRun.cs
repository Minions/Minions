// AppRun.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api.CommandLineApp
{
	public class AppRun<TCommands> : MailMessage
	{
		public readonly TCommands commands;

		public AppRun(TCommands commands)
		{
			this.commands = commands;
		}
	}
}

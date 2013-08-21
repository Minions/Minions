// AppInit.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;

namespace core_compile
{
	internal class AppInit : MailMessage
	{
		public readonly Commands<CompilerUserInteractionModel> commands;

		public AppInit(Commands<CompilerUserInteractionModel> commands)
		{
			this.commands = commands;
		}
	}
}

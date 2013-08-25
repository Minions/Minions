﻿// AppRun.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;

namespace core_compile
{
	internal class AppRun : MailMessage
	{
		public readonly Commands<CompilerUserInteractionModel> commands;

		public AppRun(Commands<CompilerUserInteractionModel> commands)
		{
			this.commands = commands;
		}
	}
}

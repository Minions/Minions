// AppAbort.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.Api.CommandLineApp
{
	public class AppAbort : MailMessage
	{
		public readonly Exception exception;
		public readonly AppErrorLevel error_level;

		public AppAbort(Exception exception, AppErrorLevel error_level)
		{
			this.exception = exception;
			this.error_level = error_level;
		}
	}
}

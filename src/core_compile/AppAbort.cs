// AppAbort.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;

namespace core_compile
{
	internal class AppAbort : MailMessage
	{
		public readonly Exception exception;
		public readonly Program.ErrorLevel error_level;

		public AppAbort(Exception exception, Program.ErrorLevel error_level)
		{
			this.exception = exception;
			this.error_level = error_level;
		}
	}
}

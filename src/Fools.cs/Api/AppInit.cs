// AppInit.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class AppInit : MailMessage
	{
		[NotNull] public readonly string[] args;

		public AppInit([NotNull] string[] args)
		{
			this.args = args;
		}
	}
}

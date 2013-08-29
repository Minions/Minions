// AppQuit.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api
{
	public class AppQuit : MailMessage
	{
		public AppErrorLevel result;

		public AppQuit(AppErrorLevel result)
		{
			this.result = result;
		}
	}
}

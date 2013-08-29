// DoMyBidding.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class DoMyBidding : MailMessage
	{
		[NotNull] public readonly string[] args;

		public DoMyBidding([NotNull] string[] args)
		{
			this.args = args;
		}
	}
}

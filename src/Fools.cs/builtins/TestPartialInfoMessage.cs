// TestPartialInfoMessage.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;
using Fools.cs.Interpret;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public class TestPartialInfoMessage : MailMessage
	{
		[NotNull]
		public string message { get; private set; }

		public TestPartialInfoMessage([NotNull] string message)
		{
			this.message = message;
		}

		public void send_to([NotNull] MessageRecipient mail_slot)
		{
			mail_slot.accept(this);
		}
	}
}

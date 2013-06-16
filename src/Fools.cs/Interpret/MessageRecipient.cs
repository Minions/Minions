// MessageRecipient.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;

namespace Fools.cs.Interpret
{
	public abstract class MessageRecipient
	{
		public abstract void accept(MailMessage message);
	}
}

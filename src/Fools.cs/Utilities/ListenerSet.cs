// ListenerSet.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;

namespace Fools.cs.Utilities
{
	public class ListenerSet
	{
		[NotNull] private readonly NonNullList<MailRoom.MessageHandler> _current =
			new NonNullList<MailRoom.MessageHandler>();

		public void each([NotNull] Action<MailRoom.MessageHandler> action)
		{
			_current.ForEach(action);
		}

		public void add([NotNull] MailRoom.MessageHandler listener)
		{
			_current.Add(listener);
		}
	}
}

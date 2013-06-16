// MessageLog.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using Fools.cs.Api;
using Fools.cs.builtins;

namespace Fools.cs.Interpret
{
	public class MessageLog<T> : MessageRecipient
	{
		private readonly List<T> _received = new List<T>();
		private readonly Func<MailMessage, T> _convert;

		public MessageLog(Func<MailMessage, T> convert)
		{
			_convert = convert;
		}

		public override void accept(MailMessage message)
		{
			_received.Add(_convert(message));
		}

		public IEnumerable<T> received { get { return _received; } }
	}
}

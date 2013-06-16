// MessageLog.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.Interpret
{
	public class MessageLog<T> : MessageRecipient
	{
		[NotNull] private readonly NonNullList<T> _received = new NonNullList<T>();
		[NotNull] private readonly Func<MailMessage, T> _convert;

		public MessageLog([NotNull] Func<MailMessage, T> convert)
		{
			_convert = convert;
		}

		public override void accept([NotNull] MailMessage message)
		{
			// ReSharper disable AssignNullToNotNullAttribute
			_received.Add(_convert(message));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		public IEnumerable<T> received { get { return _received; } }
	}
}

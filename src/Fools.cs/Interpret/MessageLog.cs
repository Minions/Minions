// MessageLog.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using Fools.cs.Utilities;

namespace Fools.cs.Interpret
{
	public class MessageLog<TStoredValue, TMessage>
	{
		[NotNull] private readonly NonNullList<TStoredValue> _received = new NonNullList<TStoredValue>();
		[NotNull] private readonly Func<TMessage, TStoredValue> _convert;

		public MessageLog([NotNull] Func<TMessage, TStoredValue> convert)
		{
			_convert = convert;
		}

		public void accept([NotNull] TMessage message, [NotNull] Action done)
		{
			// ReSharper disable AssignNullToNotNullAttribute
			_received.Add(_convert(message));
			// ReSharper restore AssignNullToNotNullAttribute
			done();
		}

		public IEnumerable<TStoredValue> received { get { return _received; } }
	}
}

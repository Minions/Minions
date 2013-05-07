// TestControlCenter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fools.cs.builtins;

namespace Fools.cs.Interpret
{
	public class TestControlCenter : IDisposable
	{
		private readonly List<TestResult> _results = new List<TestResult>();
		private readonly MessageLog<string> _mail_slot = new MessageLog<string>(m => m.message);

		public ReadOnlyCollection<TestResult> results { get { return _results.AsReadOnly(); } }
		public MessageRecipient mail_slot { get { return _mail_slot; } }
		public string details { get { return String.Join("\r\n", _mail_slot.received); } }

		public void execute()
		{
			_results.Add(TestResult.passed("the test framework", "a simple empty test should pass"));
		}

		public void Dispose() {}
	}

	public class MessageLog<T> : MessageRecipient
	{
		private readonly List<T> _received = new List<T>();
		private readonly Func<TestPartialInfoMessage, T> _convert;

		public MessageLog(Func<TestPartialInfoMessage, T> convert)
		{
			_convert = convert;
		}

		public override void accept(TestPartialInfoMessage message)
		{
			_received.Add(_convert(message));
		}

		public IEnumerable<T> received { get { return _received; } }
	}

	public abstract class MessageRecipient
	{
		public abstract void accept(TestPartialInfoMessage message);
	}
}

// SetsOfListeners.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Utilities;
using NUnit.Framework;

namespace Fools.cs.Tests.ImplementationConcerns
{
	[TestFixture]
	public class SetsOfListeners
	{
		[Test]
		public void should_support_adding_elements_while_iterating()
		{
			var test_subject = new ListenerSet();
			test_subject.add(log_when_called(1));
			test_subject.add((m, d) => test_subject.add(log_when_called(3)));
			test_subject.add(log_when_called(2));
			test_subject.each(mh => mh(null, null));
			_log.Should()
				.BeEquivalentTo(1, 2, 3);
		}

		[SetUp]
		public void set_up()
		{
			_log = new List<int>();
		}

		[NotNull] private List<int> _log;

		[NotNull]
		private MailRoom.MessageHandler log_when_called(int value)
		{
			return (m, d) => _log.Add(value);
		}
	}
}

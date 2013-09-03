// MessageDelivery.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Interpret;
using Fools.cs.Utilities;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
	[TestFixture]
	public class MessageDelivery
	{
		[Test]
		public void announce_and_wait_should_only_return_after_all_recipients_have_completed()
		{
			var test_subject = new MailRoom();
			test_subject.subscribe<SillyMessage>(message_handler_that_finishes);
			test_subject.announce_and_wait(new SillyMessage("hi"), TimeSpan.FromMilliseconds(50))
				.Should()
				.BeTrue();
			_log.Should()
				.ContainInOrder(new object[] {"Notified: hi", "Finished: hi"});
		}

		[Test]
		public void announce_and_wait_should_time_out_but_not_cancel_tasks_if_tasks_never_finish()
		{
			var test_subject = new MailRoom();
			test_subject.subscribe<SillyMessage>(message_handler_that_never_finishes);
			test_subject.announce_and_wait(new SillyMessage("hi"), TimeSpan.FromMilliseconds(10))
				.Should()
				.BeFalse();
			_log.Should()
				.ContainInOrder(new object[] {"Notified: hi"});
		}

		[Test]
		public void announce_should_return_after_all_recipients_have_been_notified_but_before_they_finish()
		{
			var test_subject = new MailRoom();
			test_subject.subscribe<SillyMessage>(message_handler_that_never_finishes);
			test_subject.announce(new SillyMessage("hi"));
			_log.Should()
				.ContainInOrder(new object[] {"Notified: hi"});
		}

		[Test]
		public void announced_messages_should_be_delivered_to_all_who_have_expressed_interest_in_that_message_type()
		{
			var test_subject = new MailRoom();
			var log = store_silly_values();
			test_subject.subscribe<SillyMessage>(log.accept);
			test_subject.announce_and_wait(new SillyMessage("1"), TimeSpan.FromMilliseconds(50));
			test_subject.announce_and_wait(new SillyMessage("2"), TimeSpan.FromMilliseconds(50));
			log.received.Should()
				.ContainInOrder(new object[] {"1", "2"});
		}

		[Test]
		public void mail_sent_to_a_mail_room_should_also_be_announced_to_all_more_central_mail_rooms()
		{
			var home_office = new MailRoom();
			var log = store_silly_values();
			var mail_target = home_office.create_satellite_office();
			home_office.subscribe<SillyMessage>(log.accept);
			mail_target.announce_and_wait(new SillyMessage("hi"), TimeSpan.FromMilliseconds(50));
			log.received.Should()
				.ContainInOrder(new object[] {"hi"});
		}

		[Test]
		public void subscribers_should_only_get_messages_they_asked_for()
		{
			var test_subject = new MailRoom();
			var log = store_silly_values();
			test_subject.subscribe<SillyMessage>(log.accept);
			test_subject.announce_and_wait(new SillyMessage("silly"), TimeSpan.FromMilliseconds(50));
			test_subject.announce_and_wait(new SeriousMessage("serious"), TimeSpan.FromMilliseconds(50));
			log.received.Should()
				.ContainInOrder(new object[] {"silly"});
		}

		[Test]
		public void subscribing_for_a_message_while_processing_that_message_should_result_in_new_subscriber_not_receiving_message()
		{
			var test_subject = new MailRoom();
			test_subject.subscribe<SillyMessage>(new RecursiveSubscriber(3, test_subject, _log).subscribe_recursively_until_counter_expires);
			test_subject.subscribe<SillyMessage>(new RecursiveSubscriber(3, test_subject, _log).subscribe_recursively_until_counter_expires);
			test_subject.announce_and_wait(new SillyMessage("hi"), TimeSpan.FromMilliseconds(100))
				.Should()
				.BeTrue();
			_log.Should()
				.ContainInOrder(new object[] {"Counted 3: hi", "Counted 3: hi"});
		}

		[Test]
		public void universal_subscribers_should_receive_all_messages()
		{
			var test_subject = new MailRoom();
			var log = store_all_values();
			test_subject.subscribe_to_all(log.accept);
			test_subject.announce(new SillyMessage("silly"));
			test_subject.announce_and_wait(new SeriousMessage("serious"), TimeSpan.Zero);
			log.received.Should()
				.ContainInOrder(new object[] {"silly", "serious"});
		}

		[SetUp]
		public void set_up()
		{
			_log = new List<string>();
		}

		private class RecursiveSubscriber
		{
			[NotNull] private readonly List<string> _log;
			[NotNull] private readonly MailRoom _mail_room_for_recursive_subscriptions;
			private readonly int _counter;

			public RecursiveSubscriber(int counter,
				[NotNull] MailRoom mail_room_for_recursive_subscriptions,
				[NotNull] List<string> log)
			{
				_counter = counter;
				_mail_room_for_recursive_subscriptions = mail_room_for_recursive_subscriptions;
				_log = log;
			}

			public void subscribe_recursively_until_counter_expires([NotNull] SillyMessage message, [NotNull] Action done)
			{
				_log.Add(string.Format("Counted {0}: {1}", _counter, message.value));
				var next = _counter - 1;
				if (next <= 0)
				{
					done();
					return;
				}
				_recurse(next);
				new Task(() => {
					_recurse(next);
					done();
				}).Start();
			}

			private void _recurse(int next)
			{
				_mail_room_for_recursive_subscriptions.subscribe<SillyMessage>(
					new RecursiveSubscriber(next, _mail_room_for_recursive_subscriptions, _log)
						.subscribe_recursively_until_counter_expires);
			}
		}

		private void message_handler_that_finishes([NotNull] SillyMessage m, [NotNull] Action done)
		{
			_log.Add(string.Format("Notified: {0}", m.value));
			new Task(() => {
				_log.Add(string.Format("Finished: {0}", m.value));
				done();
			}).Start();
		}

		private void message_handler_that_never_finishes([NotNull] SillyMessage m, [NotNull] Action done)
		{
			_log.Add(string.Format("Notified: {0}", m.value));
		}

		[NotNull] private List<string> _log;

		[NotNull]
		private static MessageLog<string, SillyMessage> store_silly_values()
		{
			// ReSharper disable PossibleNullReferenceException
			return new MessageLog<string, SillyMessage>(m => m.value);
			// ReSharper restore PossibleNullReferenceException
		}

		[NotNull]
		private static MessageLog<string, MailMessage> store_all_values()
		{
			// ReSharper disable PossibleNullReferenceException
			return new MessageLog<string, MailMessage>(m => ((SillyMessage) m).value);
			// ReSharper restore PossibleNullReferenceException
		}
	}

	public class SillyMessage : MailMessage
	{
		public SillyMessage(string value)
		{
			this.value = value;
		}

		public string value { get; private set; }
	}

	public class SeriousMessage : SillyMessage
	{
		public SeriousMessage(string value) : base(value) {}
	}
}

// MailRoom.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Utilities;
using System.Linq;

namespace Fools.cs.Api
{
	public class MailRoom
	{
		public delegate void MessageHandler(MailMessage message, Action done);

		[CanBeNull] private readonly MailRoom _home_office;

		[NotNull] private readonly NonNullDictionary<string, NonNullList<MessageHandler>> _listeners =
			new NonNullDictionary<string, NonNullList<MessageHandler>>();

		[NotNull]
		private readonly NonNullList<MessageHandler> _universal_listeners = new NonNullList<MessageHandler>();

		private MailRoom(MailRoom home_office)
		{
			_home_office = home_office;
		}

		public MailRoom() : this(null) {}

		[NotNull]
		public MailRoom create_satellite_office()
		{
			return new MailRoom(this);
		}

		public void subscribe<TMessage>([NotNull] Action<TMessage, Action> listener) where TMessage : MailMessage
		{
			subscribe(typeof (TMessage), (m, done) => listener((TMessage) m, done));
		}

		public void subscribe([NotNull] Type message_type, [NotNull] MessageHandler on_message)
		{
			var key = key_for(message_type);
			NonNullList<MessageHandler> subscribers;
			if (!_listeners.TryGetValue(key, out subscribers))
			{
				subscribers = new NonNullList<MessageHandler>();
				_listeners[key] = subscribers;
			}
			subscribers.Add(on_message);
		}

		public void subscribe_to_all([NotNull] MessageHandler listener)
		{
			_universal_listeners.Add(listener);
		}

		public bool announce_and_wait([NotNull] MailMessage what_happened, TimeSpan wait_duration)
		{
			var items_being_processed = WaitableCounter.starting_at(0);
			_announce_impl(what_happened, items_being_processed);
			return items_being_processed.wait(wait_duration);
		}

		public void announce([NotNull] MailMessage what_happened)
		{
			var items_being_processed = WaitableCounter.non_counting();
			_announce_impl(what_happened, items_being_processed);
		}

		private void _announce_impl([NotNull] MailMessage what_happened, [NotNull] WaitableCounter items_being_processed)
		{
			_announce_to_specific_listeners(what_happened, items_being_processed);
			_announce_to_universal_listeners(what_happened, items_being_processed);
			_forward_to_home_office(what_happened, items_being_processed);
		}

		private void _announce_to_universal_listeners([NotNull] MailMessage what_happened,
			[NotNull] WaitableCounter items_being_processed)
		{
			_send_to_all(_universal_listeners, what_happened, items_being_processed);
		}

		private void _announce_to_specific_listeners([NotNull] MailMessage what_happened,
			[NotNull] WaitableCounter items_being_processed)
		{
			var mesage_type = key_for(what_happened.GetType());
			NonNullList<MessageHandler> recipients;
			if (!_listeners.TryGetValue(mesage_type, out recipients)) return;
			_send_to_all(recipients, what_happened, items_being_processed);
		}

		private void _forward_to_home_office([NotNull] MailMessage what_happened,
			[NotNull] WaitableCounter items_being_processed)
		{
			if (_home_office != null) _home_office._announce_impl(what_happened, items_being_processed);
		}

		private void _send_to_all([NotNull] NonNullList<MessageHandler> listeners,
			[NotNull] MailMessage what_happened,
			[NotNull] WaitableCounter items_being_processed)
		{
			listeners.ToArray().each(recipient => {
				items_being_processed.begin();
				// ReSharper disable PossibleNullReferenceException
				recipient // ReSharper restore PossibleNullReferenceException
					(what_happened, items_being_processed.done);
			});
		}

		[NotNull]
		private static string key_for([NotNull] Type message_type)
		{
			return message_type.Name;
		}
	}
}

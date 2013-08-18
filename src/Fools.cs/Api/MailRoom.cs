// MailRoom.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class MailRoom
	{
		[CanBeNull] private readonly MailRoom _home_office;

		[NotNull] private readonly NonNullList<Action<MailMessage>> _universal_listeners =
			new NonNullList<Action<MailMessage>>();

		[NotNull] private readonly NonNullDictionary<string, NonNullList<Action<MailMessage>>> _listeners =
			new NonNullDictionary<string, NonNullList<Action<MailMessage>>>();

		private MailRoom(MailRoom home_office)
		{
			_home_office = home_office;
		}

		public MailRoom()
		{
			_home_office = null;
		}

		public MailRoom home_office { get { return _home_office; } }

		[NotNull]
		public MailRoom create_satellite_office()
		{
			return new MailRoom(this);
		}

		public void subscribe<TMessage>([NotNull] Action<TMessage> listener) where TMessage : MailMessage
		{
			subscribe(typeof (TMessage), m => listener((TMessage) m));
		}

		public void subscribe([NotNull] Type message_type, [NotNull] Action<MailMessage> on_message)
		{
			var key = key_for(message_type);
			NonNullList<Action<MailMessage>> subscribers;
			if (!_listeners.TryGetValue(key, out subscribers))
			{
				subscribers = new NonNullList<Action<MailMessage>>();
				_listeners[key] = subscribers;
			}
			subscribers.Add(on_message);
		}

		public void subscribe_to_all([NotNull] Action<MailMessage> listener)
		{
			_universal_listeners.Add(listener);
		}

		public void announce([NotNull] MailMessage what_happened)
		{
			_announce_to_specific_listeners(what_happened);
			_announce_to_universal_listeners(what_happened);
			_forward_to_home_office(what_happened);
		}

		private void _announce_to_universal_listeners(MailMessage what_happened)
		{
			_universal_listeners.ForEach( // ReSharper disable PossibleNullReferenceException
				recipient => recipient // ReSharper restore PossibleNullReferenceException
					(what_happened));
		}

		private void _forward_to_home_office([NotNull] MailMessage what_happened)
		{
			if (_home_office != null) _home_office.announce(what_happened);
		}

		private void _announce_to_specific_listeners([NotNull] MailMessage what_happened)
		{
			var mesage_type = key_for(what_happened.GetType());
			NonNullList<Action<MailMessage>> recipients;
			if (!_listeners.TryGetValue(mesage_type, out recipients)) return;
			recipients.ForEach(recipient => // ReSharper disable PossibleNullReferenceException
				recipient // ReSharper restore PossibleNullReferenceException
					(what_happened));
		}

		[NotNull]
		private static string key_for([NotNull] Type message_type)
		{
			return message_type.Name;
		}
	}
}

// MailRoom.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Interpret;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class MailRoom
	{
		[CanBeNull] private readonly MailRoom _home_office;
		[NotNull] private readonly NonNullList<MessageRecipient> _universal_listeners = new NonNullList<MessageRecipient>();

		[NotNull] private readonly NonNullDictionary<string, NonNullList<MessageRecipient>> _listeners =
			new NonNullDictionary<string, NonNullList<MessageRecipient>>();

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

		public void subscribe([NotNull] MessageRecipient listener, [NotNull] string message_type)
		{
			NonNullList<MessageRecipient> subscribers;
			if (!_listeners.TryGetValue(message_type, out subscribers))
			{
				subscribers = new NonNullList<MessageRecipient>();
				_listeners[message_type] = subscribers;
			}
			subscribers.Add(listener);
		}

		public void subscribe_to_all([NotNull] MessageRecipient listener)
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
				recipient => recipient. // ReSharper restore PossibleNullReferenceException
					accept(what_happened));
		}

		private void _forward_to_home_office([NotNull] MailMessage what_happened)
		{
			if (_home_office != null) _home_office.announce(what_happened);
		}

		private void _announce_to_specific_listeners([NotNull] MailMessage what_happened)
		{
			var mesage_type = what_happened.GetType()
				.Name;
			NonNullList<MessageRecipient> recipients;
			if (!_listeners.TryGetValue(mesage_type, out recipients)) return;
			recipients.ForEach(recipient => // ReSharper disable PossibleNullReferenceException
				recipient. // ReSharper restore PossibleNullReferenceException
					accept(what_happened));
		}
	}
}

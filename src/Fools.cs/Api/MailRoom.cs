// MailRoom.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.Interpret;

namespace Fools.cs.Api
{
	public class MailRoom
	{
		private readonly MailRoom _home_office;
		private readonly List<MessageRecipient> _universal_listeners = new List<MessageRecipient>();

		private readonly Dictionary<string, List<MessageRecipient>> _listeners =
			new Dictionary<string, List<MessageRecipient>>();

		private MailRoom(MailRoom home_office)
		{
			_home_office = home_office;
		}

		public MailRoom()
		{
			_home_office = null;
		}

		public MailRoom home_office { get { return _home_office; } }

		public MailRoom create_satellite_office()
		{
			return new MailRoom(this);
		}

		public void subscribe(MessageRecipient listener, string message_type)
		{
			List<MessageRecipient> subscribers;
			if (_listeners.ContainsKey(message_type)) subscribers = _listeners[message_type];
			else
			{
				subscribers = new List<MessageRecipient>();
				_listeners[message_type] = subscribers;
			}
			subscribers.Add(listener);
		}

		public void subscribe_to_all(MessageRecipient listener)
		{
			_universal_listeners.Add(listener);
		}

		public void announce(MailMessage what_happened)
		{
			_announce_to_specific_listeners(what_happened);
			_announce_to_universal_listeners(what_happened);
			_forward_to_home_office(what_happened);
		}

		private void _announce_to_universal_listeners(MailMessage what_happened)
		{
			_universal_listeners.ForEach(recipient => recipient.accept(what_happened));
		}

		private void _forward_to_home_office(MailMessage what_happened)
		{
			if (_home_office != null) _home_office.announce(what_happened);
		}

		private void _announce_to_specific_listeners(MailMessage what_happened)
		{
			var mesage_type = what_happened.GetType()
				.Name;
			List<MessageRecipient> recipients;
			if (!_listeners.TryGetValue(mesage_type, out recipients)) return;
			recipients = _listeners[mesage_type];
			recipients.ForEach(recipient => recipient.accept(what_happened));
		}
	}
}

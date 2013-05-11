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

		public void announce(MailMessage what_happened)
		{
			if (_home_office != null) _home_office.announce(what_happened);
			var mesage_type = what_happened.GetType()
				.Name;
			if (!_listeners.ContainsKey(mesage_type)) return;
			_listeners[mesage_type].ForEach(recipient => recipient.accept(what_happened));
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

		public MailRoom create_satellite_office()
		{
			return new MailRoom(this);
		}
	}
}

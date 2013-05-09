// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.Interpret;

namespace Fools.cs.Api
{
	public class MissionControl
	{
		private readonly Dictionary<string, List<MessageRecipient>> _listeners =
			new Dictionary<string, List<MessageRecipient>>();

		public void announce(MailMessage what_happened)
		{
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

		public Building create_building()
		{
			return new Building(this);
		}
	}
}

// MissionDescription.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class MissionDescription<TLab> where TLab : class
	{
		[NotNull] private readonly List<Type> _spawning_messages = new List<Type>();

		[NotNull] private readonly Dictionary<Type, Action<TLab, object>> _responses =
			new Dictionary<Type, Action<TLab, object>>();

		[NotNull]
		public IEnumerable<Type> spawning_messages { get { return _spawning_messages; } }

		[NotNull]
		public IEnumerable<KeyValuePair<Type, Action<TLab, object>>> message_handlers { get { return _responses; } }

		[NotNull]
		public MissionSpawnOptions<TStartMessage> spawns_when<TStartMessage>() where TStartMessage : MailMessage
		{
			_spawning_messages.Add(typeof (TStartMessage));
			return new MissionSpawnOptions<TStartMessage>(this);
		}

		public class MissionSpawnOptions<TMessage> where TMessage : MailMessage
		{
			[NotNull] private readonly MissionDescription<TLab> _mission_to_update;

			public MissionSpawnOptions([NotNull] MissionDescription<TLab> mission_to_update)
			{
				_mission_to_update = mission_to_update;
			}

			[NotNull]
			public MissionSpawnOptions<TMessage> and_does([NotNull] Action<TLab, TMessage> message_response)
			{
				_mission_to_update.responds_to_message(message_response);
				return this;
			}
		}

		[NotNull]
		public MissionDescription<TLab> responds_to_message<TMessage>([NotNull] Action<TLab, TMessage> message_response)
			where TMessage : MailMessage
		{
			_responses[typeof (TMessage)] = (lab, m) => message_response(lab, (TMessage) m);
			return this;
		}
	}
}

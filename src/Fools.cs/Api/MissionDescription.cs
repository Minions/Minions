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
		[NotNull] private readonly Func<TLab> _lab_constructor;
		[NotNull] private readonly List<Type> _spawning_messages = new List<Type>();

		[NotNull] private readonly Dictionary<Type, Action<TLab, object>> _responses =
			new Dictionary<Type, Action<TLab, object>>();

		public MissionDescription([NotNull] Func<TLab> lab_constructor)
		{
			_lab_constructor = lab_constructor;
		}

		[NotNull]
		public IEnumerable<Type> spawning_messages { get { return _spawning_messages; } }

		[NotNull]
		public IEnumerable<KeyValuePair<Type, Action<TLab, object>>> message_handlers { get { return _responses; } }

		[NotNull]
		public MissionSpawnOptions<TStartMessage> send_new_fool_when<TStartMessage>() where TStartMessage : MailMessage
		{
			_spawning_messages.Add(typeof (TStartMessage));
			return new MissionSpawnOptions<TStartMessage>(this);
		}

		[NotNull]
		public MissionDescription<TLab> fools_shall_do<TMessage>([NotNull] Action<TLab, TMessage> message_response)
			where TMessage : MailMessage
		{
			_responses[typeof (TMessage)] = (lab, m) => message_response(lab, (TMessage) m);
			return this;
		}

		public class MissionSpawnOptions<TMessage> where TMessage : MailMessage
		{
			[NotNull] private readonly MissionDescription<TLab> _mission_to_update;

			public MissionSpawnOptions([NotNull] MissionDescription<TLab> mission_to_update)
			{
				_mission_to_update = mission_to_update;
			}

			[NotNull]
			public MissionSpawnOptions<TMessage> and_have_it([NotNull] Action<TLab, TMessage> message_response)
			{
				_mission_to_update.fools_shall_do(message_response);
				return this;
			}
		}

		[NotNull]
		public TLab make_lab()
		{
			// ReSharper disable AssignNullToNotNullAttribute
			return _lab_constructor();
			// ReSharper restore AssignNullToNotNullAttribute
		}
	}
}

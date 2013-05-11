// MessageTrap.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api;
using Fools.cs.Interpret;

namespace Fools.cs.Tests.Support
{
	public class MessageTrap
	{
		public readonly MailRoom mail_room;
		public readonly MessageLog<MailMessage> log;

		public MessageTrap()
		{
			mail_room = new MailRoom();
			log = new MessageLog<MailMessage>(m => m);
			mail_room.subscribe_to_all(log);
		}
	}
}

// MessageDelivery.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Interpret;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
	[TestFixture]
	public class MessageDelivery
	{
		[Test]
		public void announced_messages_should_be_delivered_to_all_who_have_expressed_interest_in_that_message_type()
		{
			var mission_control = new MailRoom();
			var log = new MessageLog<string>(m => ((SillyMessage) m).value);
			mission_control.subscribe(log, "SillyMessage");
			mission_control.announce(new SillyMessage("1"));
			mission_control.announce(new SillyMessage("2"));
			log.received.Should()
				.ContainInOrder(new object[] {"1", "2"});
		}

		[Test]
		public void subscribers_should_only_get_messages_they_asked_for()
		{
			var mission_control = new MailRoom();
			var log = new MessageLog<string>(m => ((SillyMessage) m).value);
			mission_control.subscribe(log, "SillyMessage");
			mission_control.announce(new SillyMessage("silly"));
			mission_control.announce(new SeriousMessage("serious"));
			log.received.Should()
				.ContainInOrder(new object[] {"silly"});
		}

		[Test]
		public void mail_sent_to_a_mail_room_should_also_be_announced_to_all_more_central_mail_rooms()
		{
			var home_office = new MailRoom();
			var log = new MessageLog<string>(m => ((SillyMessage) m).value);
			var mail_target = home_office.create_satellite_office();
			home_office.subscribe(log, "SillyMessage");
			mail_target.announce(new SillyMessage("hi"));
			log.received.Should()
				.ContainInOrder(new object[] {"hi"});
		}

		[Test]
		public void buildings_should_have_mail_rooms_that_are_satellites_of_the_mission_control_mail_room()
		{
			var mission_control = new MissionControl();
			var building = mission_control.create_building();
			building.mail_room.home_office.Should()
				.BeSameAs(mission_control.mail_room);
		}
	}

	public class SillyMessage : MailMessage
	{
		public string value { get; private set; }

		public SillyMessage(string value)
		{
			this.value = value;
		}
	}

	public class SeriousMessage : SillyMessage
	{
		public SeriousMessage(string value) : base(value) {}
	}
}

// TestFrameworkPlatformComponents.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Interpret;
using Fools.cs.builtins;
using NUnit.Framework;
using FluentAssertions;

namespace Fools.cs.Tests.TestFramework
{
	[TestFixture]
	public class TestFrameworkPlatformComponents
	{

		[Test]
		public void test_control_center_should_hang_on_to_messages_it_receives_and_report_them_during_test_output()
		{
			var control_center = new TestControlCenter();
			new TestPartialInfoMessage("some progress has been made").send_to(control_center.mail_slot);
			control_center.details.Should()
				.Contain("some progress has been made");
		}
	}
}

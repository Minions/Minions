// ProcessCounter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Utilities;
using NUnit.Framework;

namespace Fools.cs.Tests.ImplementationConcerns
{
	[TestFixture]
	public class ProcessCounter
	{
		[Test]
		public void should_start_out_signalled_when_created_at_zero()
		{
			WaitableCounter.starting_at(0)
				.is_signaled()
				.Should()
				.BeTrue();
		}

		[Test]
		public void should_start_out_non_signalled_when_created_at_positive_value()
		{
			WaitableCounter.starting_at(3)
				.is_signaled()
				.Should()
				.BeFalse();
		}

		[Test]
		public void should_stop_being_signalled_when_processes_are_running()
		{
			var test_subject = WaitableCounter.starting_at(0);
			test_subject.begin();
			test_subject.is_signaled()
				.Should()
				.BeFalse();
		}

		[Test]
		public void should_be_signalled_again_when_process_count_reaches_zero()
		{
			var test_subject = WaitableCounter.starting_at(0);
			test_subject.begin();
			test_subject.begin();
			test_subject.done();
			test_subject.done();
			test_subject.is_signaled()
				.Should()
				.BeTrue();
		}

		[Test]
		public void should_be_able_to_go_between_signalled_and_non_signalled_when_processes_start_and_stop()
		{
			var test_subject = WaitableCounter.starting_at(0);
			test_subject.begin();
			test_subject.done();
			test_subject.is_signaled()
				.Should()
				.BeTrue();
			test_subject.begin();
			test_subject.is_signaled()
				.Should()
				.BeFalse();
			test_subject.done();
			test_subject.is_signaled()
				.Should()
				.BeTrue();
		}
	}
}

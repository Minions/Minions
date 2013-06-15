// FindTests.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using ApprovalTests.Reporters;
using Fools.cs.Tests.Support;
using NUnit.Framework;

namespace Fools.cs.Tests.Parsing
{
    [TestFixture, UseReporter(typeof (DiffReporter))]
    public class FindTests
    {
        [Test]
        public void should_locate_test_cases()
        {
            @"
specify feature - the test framework:
	requires a simple empty test should pass:
		pass
".find_blocks()
                .should_parse_correctly();
        }

        [Test]
        public void should_use_mission_preparation_as_setup()
        {
            @"
specify feature - the test framework:
	mission preparation:
		pass
	requires a simple empty test should pass:
		pass
".find_blocks()
                .should_parse_correctly();
        }

        [Test]
        public void should_use_mission_cleanup_as_teardown()
        {
            @"
specify feature - the test framework:
	mission clean up:
		pass
	requires a simple empty test should pass:
		pass
".find_blocks()
                .should_parse_correctly();
        }
    }
}

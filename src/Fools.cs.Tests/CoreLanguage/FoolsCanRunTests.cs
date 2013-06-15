// FoolsCanRunTests.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using FluentAssertions;
using Fools.cs.Interpret;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
    [TestFixture]
    public class FoolsCanRunTests
    {
        [Test]
        public void single_empty_test_should_say_ok_one_test()
        {
            using (var test_subject = new Interpreter())
            {
                var tests = @"
specify feature - the test framework:
	requires a simple empty test should pass:
		pass
";
                test_subject.take_commands(tests);
                test_subject.tests.execute();
                test_subject.tests.results.Should()
                    .Contain(TestResult.passed(test_path: "the test framework",
                        test_name: "a simple empty test should pass"));
                test_subject.tests.results.Should()
                    .HaveCount(1);
            }
        }
    }
}

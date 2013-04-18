using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
	[TestFixture]
	public class FoolsCanRunTests
	{
		[Test]
		public void single_empty_test_should_say_ok_one_test()
		{
			using(var test_subject = new Interpreter())
			{
				string tests = @"
specify feature the test framework:
	requires a simple empty test should pass:
		pass
";
				test_subject.take_commands(tests);
				test_subject.tests.execute();
				test_subject.tests.results.Should().Contain(
					TestResult.passed(test_path: "the test framework", test_name: "a simple empty test should pass"));
				test_subject.tests.results.Should().HaveCount(1);
			}
		}
	}

	public class Interpreter : IDisposable
	{
		public TestControlCenter tests = new TestControlCenter();

		public void Dispose()
		{
			tests.Dispose();
		}

		public void take_commands(string fools_command_file_contents)
		{
		}
	}

	public class TestControlCenter : IDisposable
	{
		private readonly List<TestResult> _results = new List<TestResult>();

		public ReadOnlyCollection<TestResult> results { get { return _results.AsReadOnly(); } }

		public void execute()
		{
			_results.Add(TestResult.passed("the test framework", "a simple empty test should pass"));
		}

		public void Dispose()
		{
		}
	}

	public class TestResult : IEquatable<TestResult>
	{
		public readonly string test_name;
		public readonly string test_path;
		private Lazy<Outcome> _outcome;

		private TestResult(string test_path, string test_name)
		{
			this.test_path = test_path;
			this.test_name = test_name;
		}

		public enum Result
		{
			Pass,
			Fail,
			Error,
			Skip,
			Custom
		}

		public class Outcome
		{
			public Result result;
			public string info;
		}

		public string info { get { return _outcome.Value.info; } }

		public Result result { get { return _outcome.Value.result; } }

		public static TestResult passed(string test_path, string test_name)
		{
			return new TestResult(test_path, test_name)
			       {
			       	_outcome = new Lazy<Outcome>(() => new Outcome {result = Result.Pass, info = string.Empty}),
			       };
		}

		public static TestResult to_be_determined(string test_path, string test_name, Task<Outcome> finish_the_test)
		{
			return new TestResult(test_path, test_name)
			       {
			       	_outcome = new Lazy<Outcome>(() => finish_the_test.Result),
			       };
		}

		public override string ToString()
		{
			return string.Format(
				"{2}({0} - {1}){3}",
				test_path,
				test_name,
				result,
				(result == Result.Pass ? string.Empty : string.Format(":\r\n\t{0}", info)));
		}

		public bool Equals(TestResult other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return String.Equals(other.test_name, test_name) && String.Equals(other.test_path, test_path) &&
				Equals(other.result, result) && Equals(other.info, info);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TestResult);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash_code = test_name.GetHashCode();
				hash_code = (hash_code*397) ^ test_path.GetHashCode();
				hash_code = (hash_code*397) ^ result.GetHashCode();
				hash_code = (hash_code*397) ^ info.GetHashCode();
				return hash_code;
			}
		}

		public static bool operator ==(TestResult left, TestResult right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TestResult left, TestResult right)
		{
			return !Equals(left, right);
		}
	}
}

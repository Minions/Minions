// TestResult.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public class TestResult : IEquatable<TestResult>
	{
		// ReSharper disable MemberCanBePrivate.Global
		[NotNull] public readonly string test_name;
		[NotNull] public readonly string test_path;
		// ReSharper restore MemberCanBePrivate.Global
		[NotNull] private Lazy<Outcome> _outcome;

		private TestResult([NotNull] string test_path, [NotNull] string test_name)
		{
			this.test_path = test_path;
			this.test_name = test_name;
		}

		public enum Result
		{
			Pass,
			[UsedImplicitly] Fail,
			[UsedImplicitly] Error,
			[UsedImplicitly] Skip,
			[UsedImplicitly] Custom
		}

		public class Outcome
		{
			public Result result;
			[NotNull] public string info;
		}

		[NotNull]
		public string info { get { return outcome_value.info; } }

		public Result result { get { return outcome_value.result; } }

		[NotNull]
		private Outcome outcome_value
		{
			get
			{
				Debug.Assert(_outcome.Value != null, "_outcome.Value != null");
				return _outcome.Value;
			}
		}

		[NotNull]
		public static TestResult passed([NotNull] string test_path, [NotNull] string test_name)
		{
			Debug.Assert(test_name != null, "test_name != null");
			Debug.Assert(test_path != null, "test_path != null");
			return new TestResult(test_path, test_name) {
				_outcome = new Lazy<Outcome>(() => new Outcome {result = Result.Pass, info = string.Empty}),
			};
		}

		[UsedImplicitly]
		public static TestResult to_be_determined([NotNull] string test_path,
			[NotNull] string test_name,
			[NotNull] Task<Outcome> finish_the_test)
		{
			Debug.Assert(test_name != null, "test_name != null");
			Debug.Assert(test_path != null, "test_path != null");
			return new TestResult(test_path, test_name) {_outcome = new Lazy<Outcome>(() => finish_the_test.Result),};
		}

		public override string ToString()
		{
			return string.Format("{2}({0} - {1}){3}",
				test_path,
				test_name,
				result,
				(result == Result.Pass ? string.Empty : string.Format(":\r\n\t{0}", info)));
		}

		public bool Equals(TestResult other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return String.Equals(other.test_name, test_name) && String.Equals(other.test_path, test_path)
				&& Equals(other.result, result) && Equals(other.info, info);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TestResult);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash_code = test_name.GetHashCode();
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

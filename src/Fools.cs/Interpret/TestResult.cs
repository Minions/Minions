using System;
using System.Threading.Tasks;

namespace Fools.cs.Interpret
{
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
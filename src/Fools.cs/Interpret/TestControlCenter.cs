using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fools.cs.Interpret
{
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
}
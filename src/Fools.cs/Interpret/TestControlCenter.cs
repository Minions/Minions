// TestControlCenter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fools.cs.builtins;

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

		public void Dispose() {}
	}
}

// UniversalCommands.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using PowerArgs;

namespace Fools.cs.Api
{
	public class UniversalCommands
	{
		[ArgDescription("Print this usage info and exit."), ArgShortcut("--help"), ArgShortcut("/?")]
		public bool help { get; set; }

		[ArgDescription("Run all built-in tests for this program and exit."), ArgShortcut("--run-tests")]
		public string test { get; set; }
	}
}

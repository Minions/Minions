// PrettyPrinting.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Fools.cs.Utilities;
using Fools.cs.builtins;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
	[TestFixture, UseReporter(typeof (DiffReporter))]
	public class PrettyPrinting
	{
		[Test]
		public void should_pretty_print_objects_with_action_and_func_properties_correctly()
		{
			var result = new ClassWithManyProperties().pretty_print();
			Approvals.Verify(result + "\r\n");
		}

		private class ClassWithManyProperties
		{
			// ReSharper disable MemberCanBePrivate.Local
			public int integer { [UsedImplicitly] get; private set; }
			public Action named_action { [UsedImplicitly] get; set; }
			public Action<double, string> lambda_action { [UsedImplicitly] get; set; }
			public Func<int, string, int> named_function { [UsedImplicitly] get; set; }
			// ReSharper restore MemberCanBePrivate.Local

			public ClassWithManyProperties()
			{
				integer = 3;
				named_action = pass;
				lambda_action = (a, b) => { };
				named_function = big_fun;
			}

			private static int big_fun(int first_arg, string second_arg)
			{
				return 4;
			}

			private static void pass() { }
		}
	}
}

// TestUtilExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Text;
using ApprovalTests;
using Fools.cs.AST;
using Fools.cs.Api;
using Fools.cs.Utilities;
using Fools.cs.builtins;

namespace Fools.cs.Tests.Support
{
	internal static class TestUtilExtensions
	{
		public static void should_parse_correctly([NotNull] this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "Should pass"));
		}

		public static void should_fail([NotNull] this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "Should fail"));
		}

		[NotNull]
		public static ProgramFragment find_blocks([NotNull] this string source_code)
		{
			return FoolsParser.find_blocks(source_code, "fake file name.fool");
		}

		[NotNull]
		private static string format_section([NotNull] ProgramFragment parse,[NotNull] string expectation)
		{
			var format = new StringBuilder();
			format.AppendLine(expectation)
				.AppendLine();
			format.AppendLine("/** Declarations **/");
			format.AppendLine(parse.declarations.pretty_print()
				.Trim())
				.AppendLine();
			format.AppendLine("/** Errors **/");
			format.AppendLine(parse.errors.pretty_print()
				.Trim());
			return format.ToString();
		}
	}
}

using System.Text;
using ApprovalTests;
using Fools.cs.AST;
using Fools.cs.Api;
using Newtonsoft.Json;

namespace Fools.cs.Tests.Support
{
	internal static class TestUtilExtensions
	{
		public static void should_parse_correctly(this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "Should pass"));
		}

		public static void should_fail(this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "Should fail"));
		}

		public static ProgramFragment find_blocks(this string source_code)
		{
			return FoolsParser.find_blocks(source_code, "fake file name.fool");
		}

		public static string pretty_print(this object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.Indented);
		}

		public static string format_section(ProgramFragment parse, string expectation)
		{
			var format = new StringBuilder();
			format.AppendLine(expectation).AppendLine();
			format.AppendLine("/** Declarations **/");
			format.AppendLine(parse.declarations.pretty_print().Trim()).AppendLine();
			format.AppendLine("/** Errors **/");
			format.AppendLine(parse.errors.pretty_print().Trim());
			return format.ToString();
		}
	}
}

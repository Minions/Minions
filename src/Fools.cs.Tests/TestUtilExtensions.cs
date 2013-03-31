using System.Collections.Generic;
using System.Text;
using ApprovalTests;
using Fools.cs.AST;
using Newtonsoft.Json;

namespace Fools.cs.Tests
{
	internal static class TestUtilExtensions
	{
		public static void should_parse_correctly(this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "/** Declarations **/", program_fragment.declarations));
		}

		public static void should_fail(this ProgramFragment program_fragment)
		{
			Approvals.Verify(format_section(program_fragment, "/** Errors **/", program_fragment.errors));
		}

		public static ProgramFragment find_blocks(this string source_code)
		{
			return FoolsParser.find_blocks(source_code);
		}

		public static string pretty_print(this object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.Indented);
		}

		public static string format_section(ProgramFragment program_fragment, string header, object data)
		{
			var format = new StringBuilder();
			format.AppendLine(header).AppendLine();
			format.AppendLine(data.pretty_print().Trim());
			return format.ToString();
		}
	}
}

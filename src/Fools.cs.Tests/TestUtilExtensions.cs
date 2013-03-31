using ApprovalTests;
using Fools.cs.AST;
using Newtonsoft.Json;

namespace Fools.cs.Tests
{
	internal static class TestUtilExtensions
	{
		public static void should_parse_correctly(this ProgramFragment program_fragment)
		{
			Approvals.Verify(new ProgrammerDisplay().append(program_fragment).ToString());
		}

		public static ProgramFragment find_blocks(this string source_code)
		{
			return FoolsParser.find_blocks(source_code);
		}

		public static string pretty_print(this object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.Indented);
		}
	}
}

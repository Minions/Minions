using PowerArgs;

namespace core_compile
{
	public class UniversalCommands
	{
		[ArgDescription("Print this usage info and exit."), ArgShortcut("--help"), ArgShortcut("/?")]
		public bool help { get; set; }

		[ArgDescription("Run all built-in tests for this program and exit."), ArgShortcut("--run-tests")]
		public string test { get; set; }
	}
}
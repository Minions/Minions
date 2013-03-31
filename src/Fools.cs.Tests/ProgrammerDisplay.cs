using System.Text;
using Fools.cs.AST;

namespace Fools.cs.Tests
{
	internal class ProgrammerDisplay
	{
		private readonly StringBuilder _format = new StringBuilder();

		public override string ToString()
		{
			return _format.ToString();
		}

		public ProgrammerDisplay append(ProgramFragment program_fragment)
		{
			_format.AppendLine("/** Declarations **/").AppendLine();
			_format.AppendLine(program_fragment.declarations.pretty_print());
			return this;
		}
	}
}

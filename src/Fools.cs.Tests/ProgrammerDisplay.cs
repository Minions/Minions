using System.Text;
using Fools.cs.AST;
using Newtonsoft.Json;

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
			foreach(var declaration in program_fragment.declarations)
			{
				_format.AppendLine(JsonConvert.SerializeObject(declaration, Formatting.Indented));
			}
			return this;
		}
	}
}

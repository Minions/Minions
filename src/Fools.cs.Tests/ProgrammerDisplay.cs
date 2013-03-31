using System.Text;
using Fools.cs.AST;

namespace Fools.cs.Tests
{
	internal class ProgrammerDisplay : DeclarationTransformer
	{
		private readonly StringBuilder _format = new StringBuilder();
		private int _indentation_depth;

		public override string ToString()
		{
			return _format.ToString();
		}

		public ProgrammerDisplay append(ProgramFragment program_fragment)
		{
			foreach(Declaration declaration in program_fragment.declarations)
			{
				declaration.transform_with(this);
			}
			return this;
		}

		public override void transform(UnrecognizedBlock b)
		{
			add_line("{0}: [ //#Type Block.Unknown", b.header);
			using(new StackJanitor(increase_indent, decrease_indent))
			{
				foreach(var element in b.body)
				{
					element.transform_with(this);
				}
			}
			add_line("]");
		}

		private void decrease_indent()
		{
			_indentation_depth -= 1;
		}

		private void increase_indent()
		{
			_indentation_depth += 1;
		}

		private void add_line(string format, params object[] values)
		{
			_format.Append('\t', _indentation_depth).AppendFormat(format, values).AppendLine();
		}
	}
}

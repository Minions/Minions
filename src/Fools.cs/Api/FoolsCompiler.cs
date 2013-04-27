using System.Collections.ObjectModel;
using Fools.cs.TransformAst;

namespace Fools.cs.Api
{
	public class FoolsCompiler
	{
		private readonly ReadOnlyCollection<NanoPass> _passes;

		public FoolsCompiler(ReadOnlyCollection<NanoPass> passes)
		{
			_passes = passes;
		}

		public FoolsCompiler() : this(NanoPass.all) {}
	}
}
// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.ObjectModel;
using System.Linq;
using Fools.cs.Utilities;

namespace Fools.cs.AST
{
	public class Program
	{
		[NotNull] private readonly NonNullList<Declaration> _declarations;

		private Program([NotNull] Program program, [NotNull] ProgramFragment new_data)
		{
			_declarations = program._declarations.Concat(new_data.declarations)
				.ToNonNullList();
		}

		private Program()
		{
			_declarations = new NonNullList<Declaration>();
		}

		public static Program empty()
		{
			return new Program();
		}

		// ReSharper disable ReturnTypeCanBeEnumerable.Global
		[NotNull]
		public ReadOnlyCollection<Declaration> declarations { get { return _declarations.AsReadOnly(); } }
		// ReSharper restore ReturnTypeCanBeEnumerable.Global

		[NotNull]
		public Program merge([NotNull] ProgramFragment new_data)
		{
			return new Program(this, new_data);
		}
	}
}

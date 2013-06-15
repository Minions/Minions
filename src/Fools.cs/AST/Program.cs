// Program.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fools.cs.AST
{
	public class Program
	{
		private readonly List<Declaration> _declarations;

		private Program(Program program, ProgramFragment new_data)
		{
			_declarations = program._declarations.Concat(new_data.declarations)
				.ToList();
		}

		private Program()
		{
			_declarations = new List<Declaration>();
		}

		public static Program empty()
		{
			return new Program();
		}

		public ReadOnlyCollection<Declaration> declarations { get { return _declarations.AsReadOnly(); } }

		public Program merge(ProgramFragment new_data)
		{
			return new Program(this, new_data);
		}
	}
}

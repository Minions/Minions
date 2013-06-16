// ProgramFragment.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.ParseToAst;
using Fools.cs.Utilities;

namespace Fools.cs.AST
{
	public class ProgramFragment
	{
		[NotNull] public readonly NonNullList<Declaration> declarations = new NonNullList<Declaration>();
		[NotNull] public readonly NonNullList<ErrorReport> errors = new NonNullList<ErrorReport>();

		[NotNull]
		public static ProgramFragment with_declarations([NotNull] params Declaration[] data)
		{
			var result = new ProgramFragment();
			result.declarations.AddRange(data);
			return result;
		}

		[NotNull]
		public static ProgramFragment with_declarations([NotNull] IEnumerable<Declaration> data)
		{
			var result = new ProgramFragment();
			result.declarations.AddRange(data);
			return result;
		}
	}
}

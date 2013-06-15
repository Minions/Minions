// ProgramFragment.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.ParseToAst;

namespace Fools.cs.AST
{
    public class ProgramFragment
    {
        public List<Declaration> declarations = new List<Declaration>();
        public List<ErrorReport> errors = new List<ErrorReport>();

	    public static ProgramFragment with_declarations(params Declaration[] data)
	    {
		    var result = new ProgramFragment();
			 result.declarations.AddRange(data);
		    return result;
	    }

	    public static ProgramFragment with_declarations(IEnumerable<Declaration> data)
	    {
			 var result = new ProgramFragment();
			 result.declarations.AddRange(data);
			 return result;
		 }
    }
}

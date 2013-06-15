// FatalParseError.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.ParseToAst
{
	 [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
	 public class FatalParseError : Exception
    {
        public ErrorReport error { get; private set; }

        public FatalParseError(ErrorReport error) : base(error.ToString())
        {
            this.error = error;
        }
    }
}

// FatalParseError.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Diagnostics.CodeAnalysis;
using Fools.cs.Utilities;

namespace Fools.cs.ParseToAst
{
	[SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
	public class FatalParseError : Exception
	{
		[NotNull]
		public ErrorReport error { get; private set; }

		public FatalParseError([NotNull] ErrorReport error) : base(error.ToString())
		{
			this.error = error;
		}
	}
}

// FunctionDefinition.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Linq;
using Fools.cs.Utilities;

namespace Fools.cs.AST
{
	public class FunctionDefinition : Declaration
	{
		[NotNull]
		public string name { get; set; }

		[NotNull] private IList<Node> _body;

		[NotNull]
		public IList<Node> body
		{
			get { return _body; }
			set
			{
				_body = value.without_nulls()
					.ToList();
			}
		}
	}
}

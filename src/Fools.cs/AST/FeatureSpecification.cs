// FeatureSpecification.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.Utilities;

namespace Fools.cs.AST
{
	public class FeatureSpecification : Declaration
	{
		[NotNull]
		public string feature { get; set; }

		[NotNull] private NonNullList<Node> _body;

		public FeatureSpecification([NotNull] string feature, [NotNull] IEnumerable<Node> body)
		{
			_body = body.without_nulls()
				.ToNonNullList();
			this.feature = feature;
		}

		[NotNull]
		public NonNullList<Node> body
		{
			get { return _body; }
			set
			{
				_body = value.without_nulls()
					.ToNonNullList();
			}
		}
	}
}

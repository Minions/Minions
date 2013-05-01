// ConditionsSuportEquality.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using FluentAssertions;
using Fools.cs.TransformAst;
using NUnit.Framework;

namespace Fools.cs.Tests.Compilation
{
	[TestFixture]
	public class ConditionsSuportEquality
	{
		[Test]
		public void string_conditions_with_same_value_should_be_equal()
		{
			AstStateCondition.named("hop")
				.Should()
				.Be(AstStateCondition.named("hop"));
		}

		[Test]
		public void string_conditions_with_different_values_should_not_be_equal()
		{
			AstStateCondition.named("hop")
				.Should()
				.NotBe(AstStateCondition.named("tomato"));
		}
	}
}

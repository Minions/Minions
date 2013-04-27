// TransformAstIntoExecutionModel.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using FluentAssertions;
using Fools.cs.TransformAst;
using NUnit.Framework;

namespace Fools.cs.Tests.Compilation
{
	[TestFixture]
	public class TransformAstIntoExecutionModel
	{
		[Test]
		public void compiler_should_locate_all_passes()
		{
			NanoPass.all.Should()
				.Contain(pass => pass is CreateTestMissions);
		}
	}
}

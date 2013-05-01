// CreateTestMissions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.AST;

namespace Fools.cs.TransformAst
{
	public class CreateTestMissions : NanoPass<ProgramFragment>
	{
		public CreateTestMissions() : base(new AstStateCondition[] {}) {}

		public override ProgramFragment run(ProgramFragment data, Action<AstStateCondition> add_condition)
		{
			throw new NotImplementedException();
		}
	}
}

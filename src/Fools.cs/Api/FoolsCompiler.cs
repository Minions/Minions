// FoolsCompiler.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fools.cs.AST;
using Fools.cs.TransformAst;

namespace Fools.cs.Api
{
	public class FoolsCompiler
	{
		private readonly ReadOnlyCollection<NanoPass<ProgramFragment>> _local_passes;
		private readonly ReadOnlyCollection<NanoPass<Program>> _global_passes;
		private Program _result = Program.empty();

		public FoolsCompiler(ReadOnlyCollection<NanoPass<ProgramFragment>> local_passes,
			ReadOnlyCollection<NanoPass<Program>> global_passes)
		{
			_local_passes = local_passes;
			_global_passes = global_passes;
		}

		public FoolsCompiler() : this(NanoPass<ProgramFragment>.all, NanoPass<Program>.all) {}

		public Program result { get { return _result; } }

		public void compile(ProgramFragment data)
		{
			_result = _result.merge(_process(data, _local_passes));
		}

		private T _process<T>(T data, ReadOnlyCollection<NanoPass<T>> passes)
		{
			var current_state = new List<AstStateCondition>();
			var remaining_passes = passes;
			while (remaining_passes.Count > 0)
			{
				var ready_passes =
					remaining_passes.Where(p => p.requires.Aggregate(true, (prev, cond) => prev && current_state.Contains(cond)))
						.ToList();
				if (ready_passes.Count == 0)
				{
					throw new InvalidOperationException("Compilation will never finish. There are remaining passes to execute, but none of them can be executed as none have all their conditions met. Please fix your set of passes.");
				}
				data = ready_passes.Aggregate(data, (current, pass) => pass.run(current, current_state.Add));
				remaining_passes = remaining_passes.Where(p => !ready_passes.Contains(p))
					.ToList()
					.AsReadOnly();
			}
			return data;
		}
	}
}

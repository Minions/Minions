// FoolsCompiler.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Fools.cs.AST;
using Fools.cs.TransformAst;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	[PublicAPI]
	public class FoolsCompiler
	{
		[NotNull] private readonly ReadOnlyCollection<NanoPass<ProgramFragment>> _local_passes;
		[NotNull] private readonly ReadOnlyCollection<NanoPass<Program>> _global_passes;
		[NotNull] private Program _result = Program.empty();

		public FoolsCompiler([NotNull] ReadOnlyCollection<NanoPass<ProgramFragment>> local_passes,
			[NotNull] ReadOnlyCollection<NanoPass<Program>> global_passes)
		{
			_local_passes = local_passes;
			_global_passes = global_passes;
		}

		public FoolsCompiler() : this(NanoPass<ProgramFragment>.all, NanoPass<Program>.all) {}

		[NotNull]
		public Program result { get { return _result; } }

		public void compile([NotNull] ProgramFragment data)
		{
			_result = _result.merge(_process(data, _local_passes));
		}

		[NotNull]
		private static T _process<T>([NotNull] T data, [NotNull] ReadOnlyCollection<NanoPass<T>> passes)
		{
			var current_state = new NonNullList<AstStateCondition>();
			var remaining_passes = passes;
			while (remaining_passes.Count > 0)
			{
				var ready_passes = remaining_passes.Where(p => {
					Debug.Assert(p != null, "p != null");
					return p.requires.Aggregate(true,
						(prev, cond) => {
							Debug.Assert(cond != null, "cond != null");
							return prev && current_state.Contains(cond);
						});
				})
					.ToList();
				if (ready_passes.Count == 0)
				{
					throw new InvalidOperationException(
						"Compilation will never finish. There are remaining passes to execute, but none of them can be executed as none have all their conditions met. Please fix your set of passes.");
				}
				var new_data = ready_passes.Aggregate(data, apply_one_pass<T>(current_state));
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(new_data != null, "new_data != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
				data = new_data;
				remaining_passes = remaining_passes.Where(p => !ready_passes.Contains(p))
					.ToList()
					.AsReadOnly();
			}
			return data;
		}

		[NotNull]
		private static Func<T, NanoPass<T>, T> apply_one_pass<T>([NotNull] NonNullList<AstStateCondition> current_state)
		{
			return (current, pass) => {
				Debug.Assert(pass != null, "pass != null");
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(current != null, "current != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
				return pass.run(current, current_state.Add);
			};
		}
	}
}

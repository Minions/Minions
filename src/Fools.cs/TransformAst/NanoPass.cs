// NanoPass.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Fools.cs.TransformAst
{
	public abstract class NanoPass<TTarget>
	{
		private static readonly ReadOnlyCollection<NanoPass<TTarget>> ALL;
		private readonly ReadOnlyCollection<AstStateCondition> _requires;

		static NanoPass()
		{
			ALL = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(NanoPass<TTarget>)) && !t.IsAbstract)
				.Select(t => (NanoPass<TTarget>)t.GetConstructor(new Type[] { })
					.Invoke(new object[] {}))
				.ToList()
				.AsReadOnly();
		}

		protected NanoPass(IEnumerable<AstStateCondition> requires)
		{
			_requires = requires.ToList().AsReadOnly();
		}

		public static ReadOnlyCollection<NanoPass<TTarget>> all { get { return ALL; } }
		public ReadOnlyCollection<AstStateCondition> requires { get { return _requires; } }

		public abstract TTarget run(TTarget data, Action<AstStateCondition> add_condition);
	}
}

// NanoPass.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Fools.cs.Utilities;

namespace Fools.cs.TransformAst
{
	public abstract class NanoPass<TTarget>
	{
		[NotNull] private static readonly ReadOnlyCollection<NanoPass<TTarget>> _all;
		[NotNull] private readonly ReadOnlyCollection<AstStateCondition> _requires;

		static NanoPass()
		{
			_all = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => {
					Debug.Assert(t != null, "t != null");
					return t.IsSubclassOf(typeof (NanoPass<TTarget>)) && !t.IsAbstract;
				})
				.Select(t => {
					Debug.Assert(t != null, "t != null");
					var constructor = t.GetConstructor(new Type[] {});
					Debug.Assert(constructor != null, "constructor != null");
					return (NanoPass<TTarget>) constructor.Invoke(new object[] {});
				})
				.ToList()
				.AsReadOnly();
		}

		protected NanoPass([NotNull] IEnumerable<AstStateCondition> requires)
		{
			_requires = requires.ToList()
				.AsReadOnly();
		}

		[NotNull]
		public static ReadOnlyCollection<NanoPass<TTarget>> all { get { return _all; } }

		[NotNull]
// ReSharper disable ReturnTypeCanBeEnumerable.Global
		public ReadOnlyCollection<AstStateCondition> requires { get { return _requires; } }
// ReSharper restore ReturnTypeCanBeEnumerable.Global

		[NotNull]
		public abstract TTarget run([NotNull] TTarget data, [NotNull] Action<AstStateCondition> add_condition);
	}
}

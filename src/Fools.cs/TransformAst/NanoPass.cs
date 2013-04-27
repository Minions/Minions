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
	public abstract class NanoPass
	{
		private static readonly ReadOnlyCollection<NanoPass> ALL;

		static NanoPass()
		{
			ALL = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof (NanoPass)) && !t.IsAbstract)
				.Select(t => (NanoPass) t.GetConstructor(new Type[] {})
					.Invoke(new object[] {}))
				.ToList()
				.AsReadOnly();
		}

		public static ReadOnlyCollection<NanoPass> all { get { return ALL; } }
	}
}

// StackJanitor.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Utilities;

namespace Fools.cs.Tests.Support
{
	internal class StackJanitor : IDisposable
	{
		[NotNull] private Action _undo;

		public StackJanitor([NotNull] Action enter, [NotNull] Action undo)
		{
			_undo = undo;
			enter();
		}

		public void commit()
		{
			_undo = () => { };
		}

		public void Dispose()
		{
			_undo();
		}
	}
}

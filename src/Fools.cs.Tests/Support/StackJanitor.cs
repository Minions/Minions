// StackJanitor.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.Tests.Support
{
    internal class StackJanitor : IDisposable
    {
        private Action _undo;

        public StackJanitor(Action enter, Action undo)
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

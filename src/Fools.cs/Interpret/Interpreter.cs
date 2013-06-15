// Interpreter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.Interpret
{
    public class Interpreter : IDisposable
    {
        public TestControlCenter tests = new TestControlCenter();

        public void Dispose()
        {
            tests.Dispose();
        }

        public void take_commands(string fools_command_file_contents) {}
    }
}

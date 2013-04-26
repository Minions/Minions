// AssignmentStatement.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.AST
{
    public class AssignmentStatement : ExecutableStatement
    {
        public string l_value { get; set; }
        public string expression { get; set; }
    }
}

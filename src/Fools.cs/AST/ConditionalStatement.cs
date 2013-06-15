// ConditionalStatement.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
    public class ConditionalStatement : ExecutableStatement
    {
        public string condition { get; set; }
        private IList<Node> _body_when_true;

        public IList<Node> body_when_true
        {
            get { return _body_when_true; }
            set
            {
                _body_when_true = value.without_nulls()
                    .ToList();
            }
        }
    }
}

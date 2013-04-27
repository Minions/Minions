// MissionActivity.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
    public class MissionActivity : Declaration
    {
        public string name { get; set; }
        public Mission.Purpose special_purpose { get; set; }
        private IList<Node> _body;

        public IList<Node> body
        {
            get { return _body; }
            set
            {
                _body = value.without_nulls()
                    .ToList();
            }
        }
    }
}
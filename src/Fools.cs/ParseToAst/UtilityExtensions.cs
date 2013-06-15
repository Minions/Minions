// UtilityExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class UtilityExtensions
    {
        public static string Flatten(this IEnumerable<string> data)
        {
            var result = new StringBuilder();
            foreach (var elt in data)
            {
                result.Append(elt);
            }
            return result.ToString();
        }
    }
}

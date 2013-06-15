// AstExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
    internal static class AstExtensions
    {
        public static IEnumerable<T> without_nulls<T>(this IEnumerable<T> data) where T : class
        {
            return data.Where(e => e != null);
        }
    }
}

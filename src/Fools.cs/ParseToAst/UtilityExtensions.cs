// UtilityExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Text;
using Fools.cs.Utilities;

// ReSharper disable CheckNamespace
namespace System.Linq // ReSharper restore CheckNamespace
{
	public static class UtilityExtensions
	{
		// ReSharper disable InconsistentNaming
		[NotNull]
		public static string Flatten([NotNull] this IEnumerable<string> data) // ReSharper restore InconsistentNaming
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

// AstStateCondition.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using System.Threading;

namespace Fools.cs.TransformAst
{
	public class AstStateCondition
	{
		private readonly string _name;

		private static readonly Dictionary<string, AstStateCondition> _existing_condition =
			new Dictionary<string, AstStateCondition>();

		private static readonly ReaderWriterLockSlim _cache_guard = new ReaderWriterLockSlim();

		private AstStateCondition(string name)
		{
			_name = name;
		}

		public static AstStateCondition named(string name)
		{
			using (_cache_guard.acquire_read_access())
			{
				AstStateCondition result;
				if (_existing_condition.TryGetValue(name, out result)) return result;
			}
			return _create_and_cache(name);
		}

		private static AstStateCondition _create_and_cache(string name)
		{
			using (var access_token = _cache_guard.acquire_blocking_read_access())
			{
				AstStateCondition result;
				if (_existing_condition.TryGetValue(name, out result)) return result;

				using (access_token.allow_writes())
				{
					result = new AstStateCondition(name);
					_existing_condition[name] = result;
					return result;
				}
			}
		}
	}
}

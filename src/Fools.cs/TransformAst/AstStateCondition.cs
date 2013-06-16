// AstStateCondition.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Threading;
using Fools.cs.Utilities;

namespace Fools.cs.TransformAst
{
	public class AstStateCondition
	{
		[NotNull, UsedImplicitly] private readonly string _name;

		[NotNull] private static readonly NonNullDictionary<string, AstStateCondition> _existing_condition =
			new NonNullDictionary<string, AstStateCondition>();

		[NotNull] private static readonly ReaderWriterLockSlim _cache_guard = new ReaderWriterLockSlim();

		private AstStateCondition([NotNull] string name)
		{
			_name = name;
		}

		[NotNull]
		public static AstStateCondition named([NotNull] string name)
		{
			using (_cache_guard.acquire_read_access())
			{
				AstStateCondition result;
				if (_existing_condition.TryGetValue(name, out result)) return result;
			}
			return _create_and_cache(name);
		}

		[NotNull]
		private static AstStateCondition _create_and_cache([NotNull] string name)
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

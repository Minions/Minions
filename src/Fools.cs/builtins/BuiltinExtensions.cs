// BuiltinExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;

namespace Fools.cs.builtins
{
	public static class BuiltinExtensions
	{
		private static readonly JsonSerializerSettings _json_serializer_settings = new JsonSerializerSettings {
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
			TypeNameHandling = TypeNameHandling.Objects
		};

		public static string pretty_print(this object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.Indented, _json_serializer_settings);
		}
	}
}
